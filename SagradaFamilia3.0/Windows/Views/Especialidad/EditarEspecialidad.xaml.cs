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

namespace SagradaFamilia3._0.Windows.Views.Especialidad
{
    /// <summary>
    /// Lógica de interacción para EditarEspecialidad.xaml
    /// </summary>
    public partial class EditarEspecialidad : Page
    {
        public ConsultorioSagradaFamilia.Models.Especialidad Especialidad { get; set; }

        public EditarEspecialidad(ConsultorioSagradaFamilia.Models.Especialidad especialidad)
        {
            InitializeComponent();

            Especialidad = especialidad;

            Nombre.Text = especialidad.Nombre;
            Habilitada.IsChecked = especialidad.Habilitada;
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            Especialidades especialidads = new Especialidades();
            Layout.Frame.Navigate(especialidads);
        }

        private void ButtonEditar_Click(object sender, RoutedEventArgs e)
        {
            if (Nombre.Text == "")
            {
                MessageBox.Show("Debe indicar un Nombre");
                return;
            }

            ConsultorioSagradaFamilia.Models.Especialidad especialidad = new ConsultorioSagradaFamilia.Models.Especialidad
            {
                IdEspecialidad = Especialidad.IdEspecialidad,
                Nombre = Nombre.Text,
                Habilitada = Habilitada.IsChecked.GetValueOrDefault()
            };

            StatusMessage statusMessage = DbContextSingleton.dbContext.EditarEspecialidad(especialidad);

            MessageBox.Show(statusMessage.Mensaje);

            if (statusMessage.Status == 0)
            {
                Especialidades especialidads = new Especialidades();
                Layout.Frame.Navigate(especialidads);
            }
        }
    }    
}
