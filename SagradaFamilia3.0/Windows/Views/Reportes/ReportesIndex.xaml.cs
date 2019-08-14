using ConsultorioSagradaFamilia.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SagradaFamilia3._0.Windows.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para ReportesIndex.xaml
    /// </summary>
    public partial class ReportesIndex : Page
    {
        public ReportesIndex()
        {
            InitializeComponent();

            MedicosCombobox.ItemsSource = DbContextSingleton.dbContext.GetMedicos();
            MedicosCombobox.DisplayMemberPath = "ApellidoNombre";
            MedicosCombobox.SelectedValuePath = "IdMedico";

            if(DatosUsuario.Rol == (int)Roles.Medico)
            {
                ObrasSocialesDeshabilitadas.Visibility = Visibility.Hidden;
                ObrasSocialesHabilitadas.Visibility = Visibility.Hidden;
                TurnosPorMedico.Visibility = Visibility.Hidden;
                PacientesEnEspera.Visibility = Visibility.Hidden;
                GananciasGenerales.Visibility = Visibility.Hidden;
                MedicosCombobox.Visibility = Visibility.Hidden;
                MedicoLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                Ganancias.Visibility = Visibility.Hidden;
            }
        }

        private bool Validar()
        {
            if (FechaDesde.SelectedDate == null)
            {
                MessageBox.Show("Debe indicar una Fecha Desde");
                return false;
            }

            if (FechaHasta.SelectedDate == null)
            {
                MessageBox.Show("Debe indicar una Fecha Hasta");
                return false;
            }

            return true;
        }

        private string GetBaseHtml(string resource, ref string filename)
        {
            string header = string.Empty;
            string body = string.Empty;
            string baseBody = string.Empty;
            string resourceName = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            resourceName = "SagradaFamilia3._0.Windows.Views.Reportes.Body.html";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                body = reader.ReadToEnd();
            }

            resourceName = "SagradaFamilia3._0.Windows.Views.Reportes.Header.html";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                header = reader.ReadToEnd();
            }

            resourceName = resource;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                baseBody = reader.ReadToEnd();
            }

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Reporte"; // Default file name
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "PDF (.pdf)|*.pdf"; // Filter files by extension

            // Show save file dialog box
            bool? result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dlg.FileName;

                header = header.Replace("[FechaDesde]", FechaDesde.SelectedDate.GetValueOrDefault().ToShortDateString());
                header = header.Replace("[FechaHasta]", FechaHasta.SelectedDate.GetValueOrDefault().ToShortDateString());

                body = body.Replace("[Reporte]", baseBody);

                string tempPath = System.IO.Path.GetTempPath();

                File.WriteAllText(tempPath + @"Header.html", header);

                return body;
            }

            return string.Empty;
        }

        private void Ganancias_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar()) return;

            if (DatosUsuario.IdUsuario == 0)
            {
                MessageBox.Show("No se esta logueado como médico");
                return;
            }

            int idMedico = DatosUsuario.IdUsuario;

            ConsultorioSagradaFamilia.Models.Medico medico = DbContextSingleton.dbContext.GetMedicoById(idMedico);

            List<PagosPorFormaPago> pagos = DbContextSingleton.dbContext.GetPagosByMedicoByFecha(idMedico, FechaDesde.SelectedDate.GetValueOrDefault(),
                                                                                                 FechaHasta.SelectedDate.GetValueOrDefault());

            string content = string.Empty;

            decimal total = 0;

            foreach (var pago in pagos)
            {
                total += pago.Monto;
                content = content +
                @"<tr>
                    <td>" + pago.FechaString + @"</td>
                    <td>" + pago.NombrePaciente + @"</td>
                    <td>" + pago.NombreObraSocial + @"</td>
                    <td>" + pago.FormaPago + @"</td>
                    <td>" + pago.Monto.ToString("c2") + @"</td>
                </tr>
                <tr>";
            }
            string filename = string.Empty;

            string html = GetBaseHtml(@"SagradaFamilia3._0.Windows.Views.Reportes.Ganancias.html", ref filename);

            html = html.Replace("[NombreMedico]", medico.Apellido + ", " + medico.Nombre);
            html = html.Replace("[Datos]", content);
            html = html.Replace("[MontoTotal]", total.ToString("c2"));

            string tempPath = System.IO.Path.GetTempPath();

            File.WriteAllText(tempPath + @"Body.html", html);

            Process.Start(@".\wkhtmltopdf.exe", tempPath + @"Body.html" + " --header-html " + tempPath + @"Header.html" + " " + filename);

            MessageBox.Show("Reporte Creado");
        }

        private void ObrasSocialesHabilitadas_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar()) return;

            List<ConsultorioSagradaFamilia.Models.ObraSocial> obrasSociales = DbContextSingleton.dbContext.GetObrasSocialesHabilitadas(1);

            string content = string.Empty;

            foreach (var item in obrasSociales)
            {
                content = content +
                @"<tr>
                    <td>" + item.Nombre + @"</td>
                </tr>
                <tr>";
            }
            string filename = string.Empty;

            string html = GetBaseHtml(@"SagradaFamilia3._0.Windows.Views.Reportes.ObrasSocialesHabilitadas.html", ref filename);

            html = html.Replace("[Datos]", content);

            string tempPath = System.IO.Path.GetTempPath();

            File.WriteAllText(tempPath + @"Body.html", html);

            Process.Start(@".\wkhtmltopdf.exe", tempPath + @"Body.html" + " --header-html " + tempPath + @"Header.html" + " " + filename);

            MessageBox.Show("Reporte Creado");
        }

        private void ObrasSocialesDeshabilitadas_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar()) return;

            List<ConsultorioSagradaFamilia.Models.ObraSocial> obrasSociales = DbContextSingleton.dbContext.GetObrasSocialesHabilitadas(0);

            string content = string.Empty;

            foreach (var item in obrasSociales)
            {
                content = content +
                @"<tr>
                    <td>" + item.Nombre + @"</td>
                </tr>
                <tr>";
            }
            string filename = string.Empty;

            string html = GetBaseHtml(@"SagradaFamilia3._0.Windows.Views.Reportes.ObrasSocialesHabilitadas.html", ref filename);

            html = html.Replace("[Datos]", content);

            string tempPath = System.IO.Path.GetTempPath();

            File.WriteAllText(tempPath + @"Body.html", html);

            Process.Start(@".\wkhtmltopdf.exe", tempPath + @"Body.html" + " --header-html " + tempPath + @"Header.html" + " " + filename);

            MessageBox.Show("Reporte Creado");
        }

        private void TurnosPorMedico_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar()) return;

            int idMedico = DatosUsuario.IdUsuario;

            if(idMedico == 0)
            {
                idMedico = (int)MedicosCombobox.SelectedValue;
            }

            ConsultorioSagradaFamilia.Models.Medico medico = DbContextSingleton.dbContext.GetMedicoById(idMedico);

            List<TurnosPorPaciente> items = DbContextSingleton.dbContext.GetTurnosByMedicoByFecha(idMedico, FechaDesde.SelectedDate.GetValueOrDefault(),
                                                                                                 FechaHasta.SelectedDate.GetValueOrDefault());
            string content = string.Empty;
            
            foreach (var item in items)
            {
                content = content +
                @"<tr>
                    <td>" + item.FechaString + @"</td>
                    <td>" + item.NombrePaciente + @"</td>
                </tr>
                <tr>";
            }
            string filename = string.Empty;

            string html = GetBaseHtml(@"SagradaFamilia3._0.Windows.Views.Reportes.Turnos.html", ref filename);

            html = html.Replace("[NombreMedico]", medico.Apellido + ", " + medico.Nombre);
            html = html.Replace("[Datos]", content);

            string tempPath = System.IO.Path.GetTempPath();

            File.WriteAllText(tempPath + @"Body.html", html);

            Process.Start(@".\wkhtmltopdf.exe", tempPath + @"Body.html" + " --header-html " + tempPath + @"Header.html" + " " + filename);

            MessageBox.Show("Reporte Creado");
        }

        private void PacientesEnEspera_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar()) return;

            List<TurnosPorPaciente> items = DbContextSingleton.dbContext.GetPacientesEnEspera(FechaDesde.SelectedDate.GetValueOrDefault(),
                                                                                              FechaHasta.SelectedDate.GetValueOrDefault());
            string content = string.Empty;

            foreach (var item in items)
            {
                content = content +
                @"<tr>
                    <td>" + item.FechaString + @"</td>
                    <td>" + item.NombrePaciente + @"</td>
                    <td>" + item.NombreMedico + @"</td>
                </tr>
                <tr>";
            }
            string filename = string.Empty;

            string html = GetBaseHtml(@"SagradaFamilia3._0.Windows.Views.Reportes.PacientesEnEspera.html", ref filename);

            html = html.Replace("[Datos]", content);

            string tempPath = System.IO.Path.GetTempPath();

            File.WriteAllText(tempPath + @"Body.html", html);

            Process.Start(@".\wkhtmltopdf.exe", tempPath + @"Body.html" + " --header-html " + tempPath + @"Header.html" + " " + filename);

            MessageBox.Show("Reporte Creado");
        }

        private void GananciasGenerales_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar()) return;            

            List<PagosPorFormaPago> pagos = DbContextSingleton.dbContext.GetPagosByFecha(FechaDesde.SelectedDate.GetValueOrDefault(), FechaHasta.SelectedDate.GetValueOrDefault());
            string content = string.Empty;

            decimal total = 0;

            foreach (var pago in pagos)
            {
                total += pago.Monto;
                content = content +
                @"<tr>
                    <td>" + pago.FechaString + @"</td>
                    <td>" + pago.NombreMedico + @"</td>
                    <td>" + pago.NombrePaciente + @"</td>
                    <td>" + pago.NombreObraSocial + @"</td>
                    <td>" + pago.FormaPago + @"</td>
                    <td>" + pago.Monto.ToString("c2") + @"</td>
                </tr>
                <tr>";
            }
            string filename = string.Empty;

            string html = GetBaseHtml(@"SagradaFamilia3._0.Windows.Views.Reportes.GananciasGenerales.html", ref filename);

            html = html.Replace("[Datos]", content);
            html = html.Replace("[MontoTotal]", total.ToString("c2"));

            string tempPath = System.IO.Path.GetTempPath();

            File.WriteAllText(tempPath + @"Body.html", html);

            Process.Start(@".\wkhtmltopdf.exe", tempPath + @"Body.html" + " --header-html " + tempPath + @"Header.html" + " " + filename);

            MessageBox.Show("Reporte Creado");
        }

        //Sirve tanto para mostrar todos los pacientes como para mostrar los pacientes de un medico en especifico
        private void PacientesPorObraSocial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TurnosDiariosPorMedico_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PacientesProximaConsulta_Click(object sender, RoutedEventArgs e)
        {

        }

        //Sirve tanto para mostrar todos los pagos como para mostrar los pagos de algun medico en especifico
        private void PagosPorModalidad_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
