using SagradaFamilia3._0.Models;
using SagradaFamilia3._0.Views;
using SagradaFamilia3._0.Windows.Views.Medico;
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
    /// Lógica de interacción para Medicos.xaml
    /// </summary>
    public partial class Medicos : Page
    {
        public List<ConsultorioSagradaFamilia.Models.Medico> medicos = new List<ConsultorioSagradaFamilia.Models.Medico>();
        
        public Medicos()
        {
            InitializeComponent();

            medicos = DbContextSingleton.dbContext.GetMedicos();
            MedicosGrid.ItemsSource = medicos;
            MedicosGrid.IsReadOnly = true;
        }

        private void ButtonNuevo_Click(object sender, RoutedEventArgs e)
        {
            Crear crearMedico = new Crear();
            Layout.Frame.Navigate(crearMedico);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConsultorioSagradaFamilia.Models.Medico seleccion = (ConsultorioSagradaFamilia.Models.Medico)MedicosGrid.SelectedItem;
            
            if(seleccion == null)
            {
                MessageBox.Show("Seleccione un médico");
                return;
            }

            EditarMedico editarMedico = new EditarMedico(seleccion);           

            Layout.Frame.Navigate(editarMedico);
        }
    }
}
