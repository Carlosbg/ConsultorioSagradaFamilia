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
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Threading;

namespace SagradaFamilia3._0
{
    public static class DatosUsuario
    {
        public static string NombreUsuario { get; set; }
        public static string Token { get; set; }
    }

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
            //Logeo pistola rapido para probar
            var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/token");

            var request = new RestRequest("", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", "carlosbenitezgiuggia@gmail.com");
            request.AddParameter("password", "Carlos@123456");
            
            IRestResponse response = client.Execute(request);
            var content = response.Content;

            dynamic stuff = JObject.Parse(content);

            DatosUsuario.Token = stuff.access_token;
            
            if (Medic.IsChecked == true)
            {
                Medico ventana = new Medico();
                ventana.Show();
                ventana.Focus();
                this.Close();
            }
            else
            {
                Administrador ventana = new Administrador();
                ventana.Show();
                ventana.Focus();
                this.Close();
            }

            //Logeo funcional
            //if (Admin.IsChecked != true && Medic.IsChecked != true)
            //{
            //    MessageBox.Show("Debe indicar su rol");
            //    return;
            //}

            //var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/token");

            //var request = new RestRequest("", Method.POST);
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //request.AddParameter("grant_type", "password");
            //request.AddParameter("username", Usuario.Text);
            //request.AddParameter("password", Contraseña.Password);

            //// execute the request
            //IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string

            //dynamic stuff = JObject.Parse(content);

            //if (stuff.error == null)
            //{               
            //    DatosUsuario.Token = stuff.access_token;

            //    if (Admin.IsChecked == true)
            //    {
            //        Administrador ventana = new Administrador();
            //        ventana.Show();
            //        ventana.Focus();
            //    }
            //    else if (Medic.IsChecked == true)
            //    {
            //        Medico ventana = new Medico();
            //        ventana.Show();
            //        ventana.Focus();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Usuario y/o contraseña inválidos");
            //}
        }
    }
}
