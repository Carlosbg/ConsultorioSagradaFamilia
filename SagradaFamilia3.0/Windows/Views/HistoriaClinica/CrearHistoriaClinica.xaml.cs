using SagradaFamilia3._0.Models;
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

namespace SagradaFamilia3._0.Windows.Views.HistoriaClinica
{
    /// <summary>
    /// Lógica de interacción para CrearHistoriaClinica.xaml
    /// </summary>
    public partial class CrearHistoriaClinica : Page
    {
        public int IdPaciente { get; set; }
        public string PacienteApellidoNombre { get; set; }

        public CrearHistoriaClinica(int idPaciente, string pacienteApellidoNombre)
        {
            InitializeComponent();
            IdPaciente = idPaciente;
            Paciente.Text = pacienteApellidoNombre;
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            HistoriaClinica historiaClinica = new HistoriaClinica();
            Layout.Frame.Navigate(historiaClinica);
        }

        private void ButtonCrear_Click(object sender, RoutedEventArgs e)
        {
            if(Fecha.SelectedDate == null)
            {
                MessageBox.Show("Debe indicar una fecha");
                return;
            }

            TextRange textRange = new TextRange(          
                Observaciones.Document.ContentStart,
                Observaciones.Document.ContentEnd
            );

            ConsultorioSagradaFamilia.Models.HistoriaClinica historiaClinica = new ConsultorioSagradaFamilia.Models.HistoriaClinica
            {
                Fecha = Fecha.SelectedDate.Value.Date,
                IdMedico = DatosUsuario.IdUsuario,
                IdPaciente = IdPaciente,
                Observaciones = textRange.Text
            };

            StatusMessage statusMessage = DbContextSingleton.dbContext.GuardarHistoriaClinica(historiaClinica);

            MessageBox.Show(statusMessage.Mensaje);

            if (statusMessage.Status == 0)
            {
                HistoriaClinica historiaClinicaIndex = new HistoriaClinica();
                Layout.Frame.Navigate(historiaClinicaIndex);
            }
        }
    }
}
