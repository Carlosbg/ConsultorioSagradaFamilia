using ConsultorioSagradaFamilia.Models;
using SagradaFamilia3._0.Views;
using SagradaFamilia3._0.Windows.Views.Paciente;
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

namespace SagradaFamilia3._0.Windows.Views
{
    /// <summary>
    /// Lógica de interacción para Pacientes.xaml
    /// </summary>
    public partial class Pacientes : Page
    {
        public List<ConsultorioSagradaFamilia.Models.Paciente> pacientes = new List<ConsultorioSagradaFamilia.Models.Paciente>();

        public Pacientes()
        {
            InitializeComponent();

            pacientes = DbContextSingleton.dbContext.GetPacientes();
            PacientesGrid.ItemsSource = pacientes;
            PacientesGrid.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CrearPaciente pacientes = new CrearPaciente();
            Layout.Frame.Navigate(pacientes);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ConsultorioSagradaFamilia.Models.Paciente seleccion = (ConsultorioSagradaFamilia.Models.Paciente)PacientesGrid.SelectedItem;

            if (seleccion == null)
            {
                MessageBox.Show("Seleccione un paciente");
                return;
            }
            
            EditarPaciente editarPaciente = new EditarPaciente(seleccion);

            Layout.Frame.Navigate(editarPaciente);
        }
    }
}
