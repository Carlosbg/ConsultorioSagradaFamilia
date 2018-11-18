using ConsultorioSagradaFamilia.Models;
using SagradaFamilia3._0.Views;
using SagradaFamilia3._0.Windows.Views.Banco;
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
    /// Lógica de interacción para Bancos.xaml
    /// </summary>
    public partial class Bancos : Page
    {
        public List<ConsultorioSagradaFamilia.Models.Banco> bancos = new List<ConsultorioSagradaFamilia.Models.Banco>();

        public Bancos()
        {
            InitializeComponent();

            bancos = DbContextSingleton.dbContext.GetBancos();
            BancosGrid.ItemsSource = bancos;
            BancosGrid.IsReadOnly = true;
        }

        private void CrearBanco_Click(object sender, RoutedEventArgs e)
        {
            CrearBanco crearBanco = new CrearBanco();
            Layout.Frame.Navigate(crearBanco);
        }

        private void EditarBanco_Click(object sender, RoutedEventArgs e)
        {
            ConsultorioSagradaFamilia.Models.Banco seleccion = (ConsultorioSagradaFamilia.Models.Banco)BancosGrid.SelectedItem;

            if (seleccion == null)
            {
                MessageBox.Show("Seleccione un banco");
                return;
            }

            EditarBanco editarBanco = new EditarBanco(seleccion);
            Layout.Frame.Navigate(editarBanco);
        }
    }
}
