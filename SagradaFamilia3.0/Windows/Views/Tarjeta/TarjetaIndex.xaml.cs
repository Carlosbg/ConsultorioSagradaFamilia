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

namespace SagradaFamilia3._0.Windows.Views.Tarjeta
{
    /// <summary>
    /// Lógica de interacción para TarjetaIndex.xaml
    /// </summary>
    public partial class TarjetaIndex : Page
    {
        public TarjetaIndex()
        {
            InitializeComponent();

            TarjetasGrid.ItemsSource = DbContextSingleton.dbContext.GetTarjetas();
        }

        private void CrearTarjeta_Click(object sender, RoutedEventArgs e)
        {
            CrearTarjeta crearTarjeta = new CrearTarjeta();
            Layout.Frame.Navigate(crearTarjeta);
        }

        private void EditarTarjeta_Click(object sender, RoutedEventArgs e)
        {
            //ConsultorioSagradaFamilia.Models.Tarjeta seleccion = (ConsultorioSagradaFamilia.Models.Tarjeta)TarjetasGrid.SelectedItem;

            //if (seleccion == null)
            //{
            //    MessageBox.Show("Seleccione una tarjeta");
            //    return;
            //}

            //EditarTarjeta editarTarjeta = new EditarTarjeta(seleccion);
            //Layout.Frame.Navigate(editarTarjeta);
        }

    }
}
