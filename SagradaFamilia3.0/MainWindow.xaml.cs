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
using MahApps.Metro.Controls;

namespace SagradaFamilia3._0
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void iniciarSesiion_Click(object sender, RoutedEventArgs e)
        {
            if (Admin.IsChecked == true)
            {
                Administrador ventana = new Administrador();
                ventana.Show();
                ventana.Focus();
            }
            else if (Medic.IsChecked == true) {
                Medico ventana = new Medico();
                ventana.Show();
                ventana.Focus();
            }
        }
    }
}
