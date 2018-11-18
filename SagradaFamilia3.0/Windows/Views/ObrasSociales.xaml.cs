using ConsultorioSagradaFamilia.Models;
using SagradaFamilia3._0.Views;
using SagradaFamilia3._0.Windows.Views.ObraSocial;
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
    /// Lógica de interacción para ObrasSociales.xaml
    /// </summary>
    public partial class ObrasSociales : Page
    {
        public List<ConsultorioSagradaFamilia.Models.ObraSocial> obrasSociales = new List<ConsultorioSagradaFamilia.Models.ObraSocial>();

        public ObrasSociales()
        {
            InitializeComponent();

            obrasSociales = DbContextSingleton.dbContext.GetObrasSociales();
            ObrasSocialesGrid.ItemsSource = obrasSociales;
            ObrasSocialesGrid.IsReadOnly = true;
        }

        private void CrearObraSocial_Click(object sender, RoutedEventArgs e)
        {
            CrearObraSocial crearObraSocial = new CrearObraSocial();
            Layout.Frame.Navigate(crearObraSocial);
        }

        private void EditaObraSocial_Click(object sender, RoutedEventArgs e)
        {
            ConsultorioSagradaFamilia.Models.ObraSocial seleccion = (ConsultorioSagradaFamilia.Models.ObraSocial)ObrasSocialesGrid.SelectedItem;

            if (seleccion == null)
            {
                MessageBox.Show("Seleccione una obra social");
                return;
            }

            EditarObraSocial editarObraSocial = new EditarObraSocial(seleccion);
            Layout.Frame.Navigate(editarObraSocial);
        }
    }
}
