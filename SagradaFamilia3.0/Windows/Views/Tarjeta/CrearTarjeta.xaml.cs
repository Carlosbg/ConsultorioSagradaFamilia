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

namespace SagradaFamilia3._0.Windows.Views.Tarjeta
{
    /// <summary>
    /// Lógica de interacción para CrearTarjeta.xaml
    /// </summary>
    public partial class CrearTarjeta : Page
    {
        public CrearTarjeta()
        {
            InitializeComponent();
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            TarjetaIndex tarjetas = new TarjetaIndex();
            Layout.Frame.Navigate(tarjetas);
        }

        private void ButtonCrear_Click(object sender, RoutedEventArgs e)
        {
            if (Nombre.Text == "")
            {
                MessageBox.Show("Debe indicar un Nombre");
                return;
            }

            ConsultorioSagradaFamilia.Models.Tarjeta tarjeta = new ConsultorioSagradaFamilia.Models.Tarjeta
            {
                Nombre = Nombre.Text,
                Numero = Numero.Text,
                IdBanco = (int)BancoCombobox.SelectedValue
            };

            StatusMessage statusMessage = DbContextSingleton.dbContext.GuardarTarjeta(tarjeta);

            MessageBox.Show(statusMessage.Mensaje);

            if (statusMessage.Status == 0)
            {
                TarjetaIndex obrasSociales = new TarjetaIndex();
                Layout.Frame.Navigate(obrasSociales);
            }
        }
    }
}
