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

namespace SagradaFamilia3._0.Windows.Views.Paciente
{
    /// <summary>
    /// Lógica de interacción para SolicitudesOrdenes.xaml
    /// </summary>
    public partial class SolicitudesOrdenes : Page
    {
        public List<ConsultorioSagradaFamilia.Models.SolicitudOrdenView> solicitudOrdenViews = new List<ConsultorioSagradaFamilia.Models.SolicitudOrdenView>();

        public SolicitudesOrdenes()
        {
            InitializeComponent();

            solicitudOrdenViews = DbContextSingleton.dbContext.GetSolicitudesOrden();
            SolicitudesOrdenesGrid.ItemsSource = solicitudOrdenViews;
            SolicitudesOrdenesGrid.IsReadOnly = true;
        }

        private void GenerarOrden_Click(object sender, RoutedEventArgs e)
        {
            ConsultorioSagradaFamilia.Models.SolicitudOrdenView seleccion = (ConsultorioSagradaFamilia.Models.SolicitudOrdenView)SolicitudesOrdenesGrid.SelectedItem;

            if (seleccion == null)
            {
                MessageBox.Show("Seleccione una solicitud");
                return;
            }

            if (seleccion.Orden != null && seleccion.Orden != "")
            {
                MessageBox.Show("Ya se genero una orden para esta solicitud");
                return;
            }

            RedactarOrden redactarOrden = new RedactarOrden(seleccion.IdSolicitudOrden, seleccion.IdPaciente);

            Layout.Frame.Navigate(redactarOrden);
        }
    }
}
