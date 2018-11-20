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

            if (DatosUsuario.IdUsuario == 0)
            {
                MessageBox.Show("No se esta logueado como médico");
                return false;
            }

            return true;
        }

        private void Ganancias_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar()) return;
                
            int idMedico = DatosUsuario.IdUsuario;

            ConsultorioSagradaFamilia.Models.Medico medico = DbContextSingleton.dbContext.GetMedicoById(idMedico);

            List<PagosPorFormaPago> pagos = DbContextSingleton.dbContext.GetPagosByMedicoByFecha(idMedico, FechaDesde.SelectedDate.GetValueOrDefault(),
                                                                                                 FechaHasta.SelectedDate.GetValueOrDefault());

            string header = string.Empty;
            string body = string.Empty;
            string content = string.Empty;
            string baseBody = string.Empty;

            var resourceName = string.Empty;

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

            resourceName = "SagradaFamilia3._0.Windows.Views.Reportes.Ganancias.html";

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
                string filename = dlg.FileName;


                header = header.Replace("[FechaDesde]", FechaDesde.SelectedDate.GetValueOrDefault().ToShortDateString());
                header = header.Replace("[FechaHasta]", FechaHasta.SelectedDate.GetValueOrDefault().ToShortDateString());

                baseBody = baseBody.Replace("[NombreMedico]", medico.Apellido + ", " + medico.Nombre);
                baseBody = baseBody.Replace("[Datos]", content);
                baseBody = baseBody.Replace("[MontoTotal]", total.ToString("c2"));


                body = body.Replace("[Reporte]", baseBody);

                string tempPath = System.IO.Path.GetTempPath();

                File.WriteAllText(tempPath + @"Header.html", header);
                File.WriteAllText(tempPath + @"Body.html", body);

                Process.Start(@".\wkhtmltopdf.exe", tempPath + @"Body.html" + " --header-html " + tempPath + @"Header.html" + " " + filename);

                MessageBox.Show("Reporte Creado");
            }
        }
    }
}
