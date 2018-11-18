using ConsultorioSagradaFamilia.Models;
using SagradaFamilia3._0.Views;
using SagradaFamilia3._0.Windows.Views.Especialidad;
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
    /// Lógica de interacción para Especialidades.xaml
    /// </summary>
    public partial class Especialidades : Page
    {
        public List<ConsultorioSagradaFamilia.Models.Especialidad> especialidades = new List<ConsultorioSagradaFamilia.Models.Especialidad>();

        public Especialidades()
        {
            InitializeComponent();

            especialidades = DbContextSingleton.dbContext.GetEspecialidades();
            EspecialidadesGrid.ItemsSource = especialidades;
            EspecialidadesGrid.IsReadOnly = true;
        }

        private void Crear_Click(object sender, RoutedEventArgs e)
        {
            CrearEspecialidad crearEspecialidad = new CrearEspecialidad();
            Layout.Frame.Navigate(crearEspecialidad);
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            ConsultorioSagradaFamilia.Models.Especialidad seleccion = (ConsultorioSagradaFamilia.Models.Especialidad)EspecialidadesGrid.SelectedItem;

            if (seleccion == null)
            {
                MessageBox.Show("Seleccione una especialidad");
                return;
            }

            EditarEspecialidad editarEspecialidad = new EditarEspecialidad(seleccion);
            Layout.Frame.Navigate(editarEspecialidad);
        }
    }
}
