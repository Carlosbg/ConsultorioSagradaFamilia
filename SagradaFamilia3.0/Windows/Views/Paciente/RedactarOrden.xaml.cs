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
    /// Lógica de interacción para RedactarOrden.xaml
    /// </summary>
    public partial class RedactarOrden : Page
    {
        private int _idSolicitud { get; set; }
        private int _idPaciente { get; set; }
        public RedactarOrden(int idSolicitud, int idPaciente)
        {
            InitializeComponent();
            _idSolicitud = idSolicitud;
            _idPaciente = idPaciente;
        }

        private void ButtonCrear_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(
                Orden.Document.ContentStart,
                Orden.Document.ContentEnd
            );

            DbContextSingleton.dbContext.GuardarOrden(_idSolicitud, textRange.Text, _idPaciente);
            MessageBox.Show("Orden Guardada exitosamente");
            SolicitudesOrdenes solicitudesOrdenes = new SolicitudesOrdenes();
            Layout.Frame.Navigate(solicitudesOrdenes);
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            SolicitudesOrdenes solicitudesOrdenes = new SolicitudesOrdenes();
            Layout.Frame.Navigate(solicitudesOrdenes);
        }
    }
}
