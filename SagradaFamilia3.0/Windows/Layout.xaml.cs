using SagradaFamilia3._0.Views.Pages;
using SagradaFamilia3._0.Windows.Views;
using SagradaFamilia3._0.Windows.Views.HistoriaClinica;
using SagradaFamilia3._0.Windows.Views.Usuario;
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

namespace SagradaFamilia3._0.Views
{
    /// <summary>
    /// Lógica de interacción para Layout.xaml
    /// </summary>
    public partial class Layout : Window
    {
        public static Frame Frame;

        public Layout()
        {
            InitializeComponent();
            Frame = Body;
        }

        private void Main_Initialized(object sender, EventArgs e)
        {
            Login login = new Login();
            Body.Navigate(login);
        }

        private void Btn_Medicos_Click(object sender, RoutedEventArgs e)
        {
            Medicos medicos = new Medicos();
            Body.Navigate(medicos);
        }

        private void Btn_Pacientes_Click(object sender, RoutedEventArgs e)
        {
            Pacientes pacientes= new Pacientes();
            Body.Navigate(pacientes);
        }

        private void Btn_Turnos_Click(object sender, RoutedEventArgs e)
        {
            Turnos turnos = new Turnos();
            Body.Navigate(turnos);
        }

        private void Btn_Pagos_Click(object sender, RoutedEventArgs e)
        {
            Pagos pagos = new Pagos();
            Body.Navigate(pagos);
        }

        private void Btn_ObrasSociales_Click(object sender, RoutedEventArgs e)
        {
            ObrasSociales obrasSociales = new ObrasSociales();
            Body.Navigate(obrasSociales);
        }

        private void Btn_Especialidades_Click(object sender, RoutedEventArgs e)
        {
            Especialidades especialidades = new Especialidades();
            Body.Navigate(especialidades);
        }

        private void Btn_FormasPago_Click(object sender, RoutedEventArgs e)
        {
            FormasPago formasPago = new FormasPago();
            Body.Navigate(formasPago);
        }

        private void Btn_Bancos_Click(object sender, RoutedEventArgs e)
        {
            Bancos bancos = new Bancos();
            Body.Navigate(bancos);
        }

        private void Btn_HistoriaClinica_Click(object sender, RoutedEventArgs e)
        {
            HistoriaClinica historiaClinica = new HistoriaClinica();
            Body.Navigate(historiaClinica);
        }

        private void Btn_Usuarios_Click(object sender, RoutedEventArgs e)
        {
            IndexUsuario indexUsuario = new IndexUsuario();
            Body.Navigate(indexUsuario);
        }
    }
}
