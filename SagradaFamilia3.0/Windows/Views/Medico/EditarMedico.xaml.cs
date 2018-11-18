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

namespace SagradaFamilia3._0.Windows.Views.Medico
{
    /// <summary>
    /// Lógica de interacción para EditarMedico.xaml
    /// </summary>
    public partial class EditarMedico : Page
    {
        public ConsultorioSagradaFamilia.Models.Medico Medico { get; set; }

        public EditarMedico(ConsultorioSagradaFamilia.Models.Medico medico)
        {
            InitializeComponent();
            Medico = medico;

            Apellido.Text = medico.Apellido;
            CUIL.Text = medico.CUIL;
            DNI.Text = medico.DNI.ToString();
            Domicilio.Text = medico.Domicilio;
            FechaNacimiento.SelectedDate = medico.FechaNacimiento;
            Mail.Text = medico.Mail;
            Matricula.Text = medico.Matricula.ToString();
            Monto.Text = medico.Monto.ToString("n2");
            Nombre.Text = medico.Nombre;
            Telefono.Text = medico.Telefono.ToString();
            Habilitado.IsChecked = medico.Habilitado;
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            Medicos medicos = new Medicos();
            Layout.Frame.Navigate(medicos);
        }

        private void ButtonEditar_Click(object sender, RoutedEventArgs e)
        {
            ConsultorioSagradaFamilia.Models.Medico medico = new ConsultorioSagradaFamilia.Models.Medico
            {
                IdMedico = Medico.IdMedico,
                Apellido = Apellido.Text,
                CUIL = CUIL.Text,
                DNI = int.Parse(DNI.Text),
                Domicilio = Domicilio.Text,
                FechaNacimiento = FechaNacimiento.SelectedDate.Value.Date,
                Mail = Mail.Text,
                Matricula = int.Parse(Matricula.Text),
                Monto = decimal.Parse(Monto.Text),
                Nombre = Nombre.Text,
                Telefono = int.Parse(Telefono.Text),
                Habilitado = Habilitado.IsChecked.GetValueOrDefault()
            };

            StatusMessage statusMessage = DbContextSingleton.dbContext.EditarMedico(medico);

            MessageBox.Show(statusMessage.Mensaje);

            if (statusMessage.Status == 0)
            {
                Medicos medicos = new Medicos();
                Layout.Frame.Navigate(medicos);
            }
        }

        private void TextBox_OnPreviewDNI(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);

            double d;
            e.Handled = !double.TryParse(text, out d);
        }

        private void TextBox_OnPreviewMatricula(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);

            double d;
            e.Handled = !double.TryParse(text, out d);
        }

        private void TextBox_OnPreviewCUIL(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);

            double d;
            e.Handled = !double.TryParse(text, out d);
        }

        private void TextBox_OnPreviewTelefono(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);

            double d;
            e.Handled = !double.TryParse(text, out d);
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
    }
}
