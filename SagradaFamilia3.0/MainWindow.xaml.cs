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
using SagradaFamilia3._0.Views;
using SagradaFamilia3._0.Models;

namespace SagradaFamilia3._0
{
    public enum Roles
    {
        Administrador = 1,
        DirectorMedico = 2,
        Medico = 3,
        Secretario = 4,
        Paciente = 5
    }

    public static class DatosUsuario
    {
        public static string NombreUsuario { get; set; }
        public static string Token { get; set; }
        public static int IdUsuario { get; set; }
        public static int Rol { get; set; }
    }

    public static class DbContextSingleton
    {
        public static DBContext dbContext = new DBContext();
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
            //Layout ventana = new Layout();
            //ventana.Show();
            //ventana.Focus();
            //this.Close();
            //DatosUsuario.Rol = 1;
            //return;

            #region Logeo pistola para probar
            ////Logeo pistola rapido para probar
            //var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/token");

            //var request = new RestRequest("", Method.POST);
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //request.AddParameter("grant_type", "password");
            //request.AddParameter("username", "carlosbenitezgiuggia@gmail.com");
            //request.AddParameter("password", "Carlos@123456");

            //IRestResponse response = client.Execute(request);
            //var content = response.Content;

            //dynamic stuff = JObject.Parse(content);

            //DatosUsuario.Token = stuff.access_token;

            //if (Medic.IsChecked == true)
            //{
            //    Medico ventana = new Medico();
            //    ventana.Show();
            //    ventana.Focus();
            //    this.Close();
            //}
            //else
            //{
            //    Administrador ventana = new Administrador();
            //    ventana.Show();
            //    ventana.Focus();
            //    this.Close();
            //}
            #endregion

            #region Logeo funcional

            //Logeo funcional
            if (Admin.IsChecked != true && Medic.IsChecked != true)
            {
                MessageBox.Show("Debe indicar su rol");
                return;
            }

            var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/token");

            var request = new RestRequest("", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", Usuario.Text);
            request.AddParameter("password", Contraseña.Password);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            
            dynamic stuff = JObject.Parse(content);

            if (stuff.error == null)
            {
                DatosUsuario.Token = stuff.access_token;

                int rolId = DbContextSingleton.dbContext.GetRol(Usuario.Text);

                if (rolId != 0 && rolId != (int)Roles.Paciente)
                {
                    DatosUsuario.Rol = rolId;
                    DatosUsuario.NombreUsuario = Usuario.Text;
                    
                    if(rolId == (int)Roles.Medico)
                    {
                        int medicoId = DbContextSingleton.dbContext.GetMedicoIdByMail(Usuario.Text);
                        DatosUsuario.IdUsuario = medicoId;
                    }
                }
                else
                {
                    MessageBox.Show("Hubo un problema al identificar el usuario");
                    return;
                }
           
                Layout layout = new Layout();
                layout.Show();
                layout.Focus();
                this.Close();

                //if (Admin.IsChecked == true)
                //{
                //    Administrador ventana = new Administrador();
                //    ventana.Show();
                //    ventana.Focus();
                //}
                //else if (Medic.IsChecked == true)
                //{
                //    Medico ventana = new Medico();
                //    ventana.Show();
                //    ventana.Focus();
                //}
            }
            else
            {
                MessageBox.Show("Usuario y/o contraseña inválidos");
            }

            #endregion
        }
    }
}
