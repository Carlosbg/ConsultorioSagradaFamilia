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
using System.Windows.Shapes;

namespace SagradaFamilia3._0
{
    /// <summary>
    /// Lógica de interacción para Medico.xaml
    /// </summary>
    public partial class Medico : Window
    {
        public Medico()
        {
            InitializeComponent();
            limpiarPantalla();
        }

        private void butDesconectar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            main.Focus();
            this.Close();
        }

        private void limpiarPantalla() {

            PacienteBox.IsEnabled = false;
            PacienteBox.Visibility = Visibility.Hidden;

            butAbrir.IsEnabled = false;
            butAbrir.Visibility = Visibility.Hidden;

            butHistoria.IsEnabled = false;
            butHistoria.Visibility = Visibility.Hidden;

            lista.IsEnabled = false;
            lista.Visibility = Visibility.Hidden;

            butAtender.IsEnabled = false;
            butAtender.Visibility = Visibility.Hidden;

            butBuscar.IsEnabled = false;
            butBuscar.Visibility = Visibility.Hidden;

            butAnular.IsEnabled = false;
            butAnular.Visibility = Visibility.Hidden;

        }

        private void butTurnosHoy_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();

            lista.IsEnabled = true;
            lista.Visibility = Visibility.Visible;

            //butAtender.IsEnabled = true;
            butAtender.Visibility = Visibility.Visible;
            
            //butAnular.IsEnabled = true;
            butAnular.Visibility = Visibility.Visible;

            //descomentar atender y anular recien cuando se selecciona un turno de la lista

        }

        private void butCalendarioDeTurnos_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();
            lista.IsEnabled = true;
            lista.Visibility = Visibility.Visible;


        }



        private void butHistoria_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();
            lista.IsEnabled = true;
            lista.Visibility = Visibility.Visible;

            butAbrir.IsEnabled = true;
            butAbrir.Visibility = Visibility.Visible;
        }

        private void butMisPacientes_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();

            //butHistoria.IsEnabled = true;
            butHistoria.Visibility = Visibility.Visible;
            //habilitar historia clinica cuando se selecicona un paciente
            lista.IsEnabled = true;
            lista.Visibility = Visibility.Visible;

            butBuscar.IsEnabled = true;
            butBuscar.Visibility = Visibility.Visible;

            PacienteBox.IsEnabled = true;
            PacienteBox.Visibility = Visibility.Visible;

        }

        private void butBuscar_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
