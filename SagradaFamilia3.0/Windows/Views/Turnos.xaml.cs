using ConsultorioSagradaFamilia.Models;
using SagradaFamilia3._0.Views;
using SagradaFamilia3._0.Windows.Views.Turno;
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
    /// Lógica de interacción para Turnos.xaml
    /// </summary>
    public partial class Turnos : Page
    {
        public List<TurnosPorPaciente> turnos = new List<TurnosPorPaciente>();

        public Turnos()
        {
            InitializeComponent();

            turnos = DbContextSingleton.dbContext.GetTurnos();
            TurnosGrid.ItemsSource = turnos;
            TurnosGrid.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CrearTurno crearTurno = new CrearTurno();
            Layout.Frame.Navigate(crearTurno);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TurnosPorPaciente seleccion = (TurnosPorPaciente)TurnosGrid.SelectedItem;
            if (seleccion.Atendido)
            {
                MessageBox.Show("Este turno no puede ser editado porque ya fue atendido.");
                return;
            }
            editarTurno(seleccion.IdTurno);
        }

        private void editarTurno(int idTurno)
        {
            CrearTurno crearTurno = new CrearTurno(idTurno);
            Layout.Frame.Navigate(crearTurno);
            //cargarCrearTurno();//Cargar pantalla en layout

            //CargarTurnoParaEditar(idTurno); //En la otra pantalla
        }

        private void Button_Pago_Click(object sender, RoutedEventArgs e)
        {
            TurnosPorPaciente seleccion = (TurnosPorPaciente)TurnosGrid.SelectedItem;

            if(seleccion == null)
            {
                MessageBox.Show("Debe seleccionar un turno.");
                return;
            }

            CrearPago crearPago = new CrearPago(seleccion.IdTurno, seleccion.NombreMedico, seleccion.NombrePaciente, seleccion.IdPaciente);
            Layout.Frame.Navigate(crearPago);
        }
    }
}
