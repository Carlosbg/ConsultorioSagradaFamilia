using ConsultorioSagradaFamilia.Models;
using SagradaFamilia3._0.Models;
using SagradaFamilia3._0.Utilities;
using SagradaFamilia3._0.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SagradaFamilia3._0.Windows.Views.Paciente
{
    /// <summary>
    /// Lógica de interacción para EditarPaciente.xaml
    /// </summary>
    public partial class EditarPaciente : Page
    {
        public ConsultorioSagradaFamilia.Models.Paciente Paciente { get; set; }
        public List<ConsultorioSagradaFamilia.Models.ObraSocial> ObrasSociales { get; set; }

        public List<ConsultorioSagradaFamilia.Models.ObraSocial> ObrasSocialesOriginales { get; set; }

        public EditarPaciente(ConsultorioSagradaFamilia.Models.Paciente paciente)
        {
            InitializeComponent();
            Paciente = paciente;

            Apellido.Text = paciente.Apellido;
            DNI.Text = paciente.DNI.ToString();
            Domicilio.Text = paciente.Direccion;
            FechaNacimiento.SelectedDate = paciente.FechaNacimiento;
            Nombre.Text = paciente.Nombre;
            
            ObrasSociales = DbContextSingleton.dbContext.GetObraSocialesPorPaciente(Paciente.IdPaciente);
            ObrasSocialesOriginales = DbContextSingleton.dbContext.GetObraSocialesPorPaciente(Paciente.IdPaciente);
            
            ObrasSocialesGrid.ItemsSource = ObrasSociales;
        }

        private void ObrasSocialesCombobox_Initialized(object sender, EventArgs e)
        {
            ObrasSocialesCombobox.ItemsSource = DbContextSingleton.dbContext.GetObrasSociales();

            ObrasSocialesCombobox.DisplayMemberPath = "Nombre";
            ObrasSocialesCombobox.SelectedValuePath = "IdObraSocial";
        }

        private void AgregarObraSocial_Click(object sender, RoutedEventArgs e)
        {
            var obraSocial = (ConsultorioSagradaFamilia.Models.ObraSocial)ObrasSocialesCombobox.SelectedItem;

            if (obraSocial != null)
            {
                foreach (var item in ObrasSocialesGrid.Items)
                {
                    if (((ConsultorioSagradaFamilia.Models.ObraSocial)item).IdObraSocial == obraSocial.IdObraSocial)
                    {
                        MessageBox.Show("Ya se seleccionó esta obra social");
                        return;
                    }
                }

                ObrasSociales.Add(obraSocial);

                ObrasSocialesGrid.ItemsSource = null;
                ObrasSocialesGrid.ItemsSource = ObrasSociales;
            }
        }

        private void BorrarObraSocial_Click(object sender, RoutedEventArgs e)
        {
            ConsultorioSagradaFamilia.Models.ObraSocial seleccion = (ConsultorioSagradaFamilia.Models.ObraSocial)ObrasSocialesGrid.SelectedItem;

            if (seleccion == null)
            {
                MessageBox.Show("Debe seleccionar una obra social");
                return;
            }

            foreach (var item in ObrasSociales)
            {
                if (item.IdObraSocial == seleccion.IdObraSocial)
                {
                    ObrasSociales.Remove(item);

                    ObrasSocialesGrid.ItemsSource = null;
                    ObrasSocialesGrid.ItemsSource = ObrasSociales;

                    break;
                }
            }
        }

        private void ButtonCrear_Click(object sender, RoutedEventArgs e)
        {
            if (Apellido.Text == "")
            {
                MessageBox.Show("Debe indicar el Apellido");
                return;
            }
            if (DNI.Text == "")
            {
                MessageBox.Show("Debe indicar el DNI");
                return;
            }
            if (Domicilio.Text == "")
            {
                MessageBox.Show("Debe indicar el Domicilio");
                return;
            }
            if (FechaNacimiento.SelectedDate == null)
            {
                MessageBox.Show("Debe indicar la Fecha de Nacimiento");
                return;
            }
            if (Nombre.Text == "")
            {
                MessageBox.Show("Debe indicar el Nombre");
                return;
            }

            ConsultorioSagradaFamilia.Models.Paciente paciente = new ConsultorioSagradaFamilia.Models.Paciente
            {
                IdPaciente = Paciente.IdPaciente,
                Apellido = Apellido.Text,
                DNI = int.Parse(DNI.Text),
                Direccion = Domicilio.Text,
                FechaNacimiento = FechaNacimiento.SelectedDate.Value.Date,
                Nombre = Nombre.Text,
            };

            StatusMessage statusMessage = DbContextSingleton.dbContext.EditarPaciente(paciente);

            MessageBox.Show(statusMessage.Mensaje);

            if (statusMessage.Status == 0)
            {
                foreach (var item in ObrasSocialesGrid.Items)
                {
                    if (ObrasSocialesOriginales.Where(es => es.IdObraSocial ==
                       ((ConsultorioSagradaFamilia.Models.ObraSocial)item).IdObraSocial).Count() == 0)
                    {
                        ConsultorioSagradaFamilia.Models.ObraSocial obraSocial = (ConsultorioSagradaFamilia.Models.ObraSocial)item;

                        ObraSocialPaciente pacienteObraSocial = new ObraSocialPaciente();
                        pacienteObraSocial.IdPaciente = Paciente.IdPaciente;
                        pacienteObraSocial.IdObraSocial = obraSocial.IdObraSocial;

                        DbContextSingleton.dbContext.GuardarObraSocialPaciente(pacienteObraSocial);
                    }
                }

                foreach (var item in ObrasSocialesOriginales)
                {
                    if (ObrasSociales.Where(es => es.IdObraSocial ==
                        item.IdObraSocial).Count() == 0)
                    {
                        ConsultorioSagradaFamilia.Models.ObraSocial obraSocial = item;

                        ObraSocialPaciente pacienteObraSocial = new ObraSocialPaciente();
                        pacienteObraSocial.IdPaciente = Paciente.IdPaciente;
                        pacienteObraSocial.IdObraSocial = obraSocial.IdObraSocial;

                        DbContextSingleton.dbContext.BorrarObraSocialPaciente(pacienteObraSocial);
                    }
                }

                Pacientes pacientes = new Pacientes();
                Layout.Frame.Navigate(pacientes);
            }
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            Pacientes pacientes = new Pacientes();
            Layout.Frame.Navigate(pacientes);
        }

        private void Nombre_TextChanged(object sender, EventArgs e)
        {
            string textoValidado = Validations.ValidarSoloTexto(Nombre.Text);
            Nombre.Text = textoValidado;
        }

        private void Apellido_TextChanged(object sender, EventArgs e)
        {
            string textoValidado = Validations.ValidarSoloTexto(Apellido.Text);
            Apellido.Text = textoValidado;
        }

        private void TextBox_OnPreviewDNI(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);

            double d;
            e.Handled = !double.TryParse(text, out d);
        }
    }
}
