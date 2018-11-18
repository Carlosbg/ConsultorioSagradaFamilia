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
        
        public EditarPaciente(ConsultorioSagradaFamilia.Models.Paciente paciente)
        {
            InitializeComponent();
            Paciente = paciente;

            Apellido.Text = paciente.Apellido;
            DNI.Text = paciente.DNI.ToString();
            Domicilio.Text = paciente.Direccion;
            FechaNacimiento.SelectedDate = paciente.FechaNacimiento;
            Nombre.Text = paciente.Nombre;
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
