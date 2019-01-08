using ConsultorioSagradaFamilia.Models;
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
using RestSharp;
using Newtonsoft.Json.Linq;
using SagradaFamilia3._0.Views;

namespace SagradaFamilia3._0.Windows.Views.Usuario
{
    /// <summary>
    /// Lógica de interacción para CrearUsuario.xaml
    /// </summary>
    public partial class CrearUsuario : Page
    {
        public CrearUsuario()
        {
            InitializeComponent();

            IList<Rol> roles = DbContextSingleton.dbContext.GetRoles();

            roles.Remove(roles.Where(r => r.Nombre == "Médico").FirstOrDefault());

            RolesGrid.ItemsSource = roles;
            RolesGrid.IsReadOnly = true;
            RolesGrid.SelectionMode = DataGridSelectionMode.Extended;

            IList<ConsultorioSagradaFamilia.Models.Paciente> pacientes = DbContextSingleton.dbContext.GetPacientes();
            List<string> pacienteMails = new List<string>();

            foreach(var paciente in pacientes)
            {
                pacienteMails.Add(paciente.Email);
            }

            MailPacientesCombobox.ItemsSource = pacienteMails;
        }

        private void ButtonCrear_Click(object sender, RoutedEventArgs e)
        {
            if(Contraseña.Password != Confirmacion.Password)
            {
                MessageBox.Show("La confirmación y la contraseña deben ser iguales");
                return;
            }

            ConsultorioSagradaFamilia.Models.Usuario usuario = new ConsultorioSagradaFamilia.Models.Usuario
            {
                Email = MailPacientesCombobox.SelectedIndex != -1 ? (string)MailPacientesCombobox.SelectedValue : Email.Text,
                Password = Contraseña.Password
            };

            var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/api/Account/Register");
            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "ff0920db-2463-74a8-8bf4-0bf3969e4083");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");

            string body = "Email=" + usuario.Email +
                          "&Password=" + usuario.Password +
                          "&ConfirmPassword=" + Confirmacion.Password;

            var roles = RolesGrid.SelectedItems;
            int contador = 0;
            foreach (var rolVar in roles)
            {
                Rol rol = (Rol)rolVar;

                if(rol.Nombre == "Paciente")
                {
                    if (!EsPaciente.IsChecked.GetValueOrDefault() || MailPacientesCombobox.SelectedIndex == -1)
                    {
                        MessageBox.Show("No se puede seleccionar como Rol 'Paciente' si no esta indicado un mail de un paciente válido.");
                        return;
                    }
                }
                else
                {
                    if(EsPaciente.IsChecked.GetValueOrDefault() || MailPacientesCombobox.SelectedIndex != -1)
                    {
                        MessageBox.Show("No se puede seleccionar otro rol además de 'Paciente' si se indico que el usuario a crear es un paciente.");
                        return;
                    }
                }
                

                body = body + "&Roles[" + contador + "]=" + rol.Nombre;
                contador++;
            }

            request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            var content = response.Content;

            if(content != "")
            {
                dynamic stuff = JObject.Parse(content);

                if (stuff.Message.ToString() == "La solicitud no es válida.")
                {
                    foreach (var error in stuff.ModelState)
                    {
                        MessageBox.Show(error.ToString());
                        return;
                    }
                }
                else if (stuff.error == null)
                {
                    MessageBox.Show("Usuario creado.");
                    IndexUsuario indexUsuario = new IndexUsuario();
                    Layout.Frame.Navigate(indexUsuario);
                }
                else
                {
                    MessageBox.Show("Hubo un problema al registrar el usuario");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Usuario creado.");
                IndexUsuario indexUsuario = new IndexUsuario();
                Layout.Frame.Navigate(indexUsuario);
            }
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            IndexUsuario indexUsuario = new IndexUsuario();
            Layout.Frame.Navigate(indexUsuario);
        }

        private void EsPaciente_Checked(object sender, RoutedEventArgs e)
        {
            Email.Text = "";
            Email.IsEnabled = false;

            MailPacientesCombobox.IsEnabled = true;
        }

        private void EsPaciente_Unchecked(object sender, RoutedEventArgs e)
        {
            MailPacientesCombobox.SelectedIndex = -1;
            MailPacientesCombobox.IsEnabled = false;

            Email.IsEnabled = true;
        }
    }
}
