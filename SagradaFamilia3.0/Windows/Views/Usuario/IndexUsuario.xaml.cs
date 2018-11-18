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

namespace SagradaFamilia3._0.Windows.Views.Usuario
{
    /// <summary>
    /// Lógica de interacción para IndexUsuario.xaml
    /// </summary>
    public partial class IndexUsuario : Page
    {
        public IndexUsuario()
        {
            InitializeComponent();

            IList <ConsultorioSagradaFamilia.Models.Usuario> usuarios = DbContextSingleton.dbContext.GetUsuarios();
            UsuariosGrid.ItemsSource = usuarios;
            UsuariosGrid.IsReadOnly = true;
        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            CrearUsuario crearUsuario = new CrearUsuario();
            Layout.Frame.Navigate(crearUsuario);
        }
    }
}
