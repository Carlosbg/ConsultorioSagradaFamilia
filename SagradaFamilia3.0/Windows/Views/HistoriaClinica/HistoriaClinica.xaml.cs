using ConsultorioSagradaFamilia.Models;
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
    /// Lógica de interacción para HistoriaClinica.xaml
    /// </summary>
    public partial class HistoriaClinica : Page
    {
        public HistoriaClinica()
        {
            InitializeComponent();
        }

        private void PacientesCombobox_Initialized(object sender, EventArgs e)
        {
            if (DatosUsuario.Rol == (int)Roles.Medico)
            {
                PacientesCombobox.ItemsSource = DbContextSingleton.dbContext.GetPacientes(DatosUsuario.IdUsuario);
            }
            else
            {
                PacientesCombobox.ItemsSource = DbContextSingleton.dbContext.GetPacientes();
            }
            PacientesCombobox.DisplayMemberPath = "ApellidoNombre";
            PacientesCombobox.SelectedValuePath = "IdPaciente";
        }

        private void ButtonVer_Click(object sender, RoutedEventArgs e)
        {
            if(PacientesCombobox.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un paciente.");
                return;
            }

            HistoriaClinicaGrid.ItemsSource = DbContextSingleton.dbContext.GetHistoriasClinicas((int)PacientesCombobox.SelectedValue);
            HistoriaClinicaGrid.IsReadOnly = true;
        }

        private void HistoriaClinicaView_Initialized(object sender, EventArgs e)
        {
            if(DatosUsuario.Rol == (int)Roles.Medico)
            {
                ButtonNueva.Visibility = Visibility.Visible;
            }
        }

        private void ButtonNueva_Click(object sender, RoutedEventArgs e)
        {
            if (PacientesCombobox.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un paciente.");
                return;
            }
        
            CrearHistoriaClinica crearHistoriaClinica = new CrearHistoriaClinica((int)PacientesCombobox.SelectedValue, 
                ((ConsultorioSagradaFamilia.Models.Paciente)PacientesCombobox.SelectedItem).Apellido + ", " + ((ConsultorioSagradaFamilia.Models.Paciente)PacientesCombobox.SelectedItem).Nombre);
            Layout.Frame.Navigate(crearHistoriaClinica);
        }
    }
}
