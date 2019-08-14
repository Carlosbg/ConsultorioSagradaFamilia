using ConsultorioSagradaFamilia.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
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

namespace SagradaFamilia3._0.Windows.Views.Turno
{
    /// <summary>
    /// Lógica de interacción para CrearTurno.xaml
    /// </summary>
    public partial class CrearTurno : Page
    {
        private static string horarioUsadoEnEditTurno = null;
        private static int idObjetoEditando = 0;

        public CrearTurno(int idTurno = 0)
        {
            InitializeComponent();
            idObjetoEditando = idTurno;
            horarioUsadoEnEditTurno = null;
            if (idTurno != 0)
            {
                CargarTurnoParaEditar(idTurno);
                Btm_Crear.Content = "Editar";
            }
            else
            {
                Btm_Crear.Content = "Crear";
            }

        }

        private dynamic GetItem(string nombreControlador, int id)
        {
            var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/api");
            var request = new RestRequest(nombreControlador + "?id=" + id, Method.GET);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Bearer " + DatosUsuario.Token);

            var response = client.Execute(request);
            var content = response.Content;

            if (content == null) return null;
            JObject obj = JObject.Parse(content);

            dynamic dynamicObj = JObject.Parse(obj.ToString());

            return dynamicObj;
        }

        private dynamic GetLista(string nombreControlador, string id1Nombre = null, int? id1 = null,
                                 string id2Nombre = null, int? id2 = null, DateTime? fechaDesde = null,
                                 DateTime? fechaHasta = null, string nombre = null, string apellido = null,
                                 int? dni = null)
        {
            nombreControlador = nombreControlador + "?";

            if (id1Nombre != null) nombreControlador = nombreControlador + id1Nombre + "=" + id1;
            if (id2Nombre != null)
            {
                if (nombreControlador.Last() != '?')
                {
                    nombreControlador = nombreControlador + "&" + id2Nombre + "=" + id2;
                }
                else
                {
                    nombreControlador = nombreControlador + id2Nombre + "=" + id2;
                }
            }
            if (fechaDesde != null)
            {
                if (nombreControlador.Last() != '?')
                {
                    nombreControlador = nombreControlador + "&fechaDesde=" +
                        fechaDesde.GetValueOrDefault().Year + "-" +
                        fechaDesde.GetValueOrDefault().Month + "-" +
                        fechaDesde.GetValueOrDefault().Day;
                }
                else
                {
                    nombreControlador = nombreControlador + "fechaDesde=" +
                        fechaDesde.GetValueOrDefault().Year + "-" +
                        fechaDesde.GetValueOrDefault().Month + "-" +
                        fechaDesde.GetValueOrDefault().Day;
                }
            }
            if (fechaHasta != null)
            {
                if (nombreControlador.Last() != '?')
                {
                    nombreControlador = nombreControlador + "&fechaHasta=" +
                        fechaHasta.GetValueOrDefault().Year + "-" +
                        fechaHasta.GetValueOrDefault().Month + "-" +
                        fechaHasta.GetValueOrDefault().Day;
                }
                else
                {
                    nombreControlador = nombreControlador + "fechaHasta=" +
                        fechaHasta.GetValueOrDefault().Year + "-" +
                        fechaHasta.GetValueOrDefault().Month + "-" +
                        fechaHasta.GetValueOrDefault().Day;
                }
            }
            if (nombre != null)
            {
                if (nombreControlador.Last() != '?')
                {
                    nombreControlador = nombreControlador + "&";
                }
                nombreControlador = nombreControlador + "nombre=" + nombre;
            }
            if (apellido != null)
            {
                if (nombreControlador.Last() != '?')
                {
                    nombreControlador = nombreControlador + "&";
                }
                nombreControlador = nombreControlador + "apellido=" + apellido;
            }
            if (dni != null)
            {
                if (nombreControlador.Last() != '?')
                {
                    nombreControlador = nombreControlador + "&";
                }
                nombreControlador = nombreControlador + "dni=" + dni;
            }

            if (nombreControlador.Last() == '?') nombreControlador = nombreControlador.Trim('?');

            var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/api");
            var request = new RestRequest(nombreControlador, Method.GET);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Bearer " + DatosUsuario.Token);

            var response = client.Execute(request);
            var content = response.Content;

            if (content == null) return null;
            JArray rss = JArray.Parse(content);

            var lista = new List<dynamic>();

            foreach (var JToken in rss)
            {
                dynamic dynamicObj;
                try
                {
                    dynamicObj = JObject.Parse(JToken.ToString());
                }
                catch (Exception e)
                {
                    dynamicObj = JToken.ToString();
                }

                lista.Add(dynamicObj);
            }

            return lista;
        }

        private void Especialidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillMedicos();
        }

        private void Medico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillDias();
        }

        private void Fecha_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillHorarios();
        }

        private void TurnoM_Checked()
        {
            if (Fecha.SelectedValue == null) return;
            FillHorarios();
        }

        private void TurnoT_Checked()
        {
            if (Fecha.SelectedValue == null) return;
            FillHorarios();
        }

        private void TurnoE_Checked(object sender, RoutedEventArgs e)
        {
            hor.IsEnabled = false;
            fech.IsEnabled = false;
            Hora.IsEnabled = false;
            Fecha.IsEnabled = false;
        }

        private void TurnoM_Checked(object sender, RoutedEventArgs e)
        {
            TurnoM.IsEnabled = true;
            TurnoT.IsEnabled = true;
            hor.IsEnabled = true;
            fech.IsEnabled = true;
            Hora.IsEnabled = true;
            Fecha.IsEnabled = true;
            TurnoM_Checked();
        }

        private void TurnoT_Checked(object sender, RoutedEventArgs e)
        {
            TurnoM.IsEnabled = true;
            TurnoT.IsEnabled = true;
            hor.IsEnabled = true;
            fech.IsEnabled = true;
            Hora.IsEnabled = true;
            Fecha.IsEnabled = true;
            TurnoT_Checked();
        }

        private void Hora_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Paciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<dynamic> medicoDynamicList = GetLista("Medico", "idEspecialidad",
                                                        Especialidad.SelectedValue != null ? (int?)Especialidad.SelectedValue : null,
                                                       "idPaciente", null);
                                                       //Paciente.SelectedValue != null ? (int?)Paciente.SelectedValue : null);
            List<ConsultorioSagradaFamilia.Models.Medico> medicoList = new List<ConsultorioSagradaFamilia.Models.Medico>();

            foreach (var medicoDynamic in medicoDynamicList)
            {
                ConsultorioSagradaFamilia.Models.Medico medico = new ConsultorioSagradaFamilia.Models.Medico
                {
                    Nombre = medicoDynamic.Nombre,
                    IdMedico = medicoDynamic.IdMedico,
                    Apellido = medicoDynamic.Apellido
                };

                medicoList.Add(medico);
            }

            Medico.ItemsSource = medicoList;
            Medico.DisplayMemberPath = "ApellidoNombre";
            Medico.SelectedValuePath = "IdMedico";
        }

        private void Paciente_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Paciente.ItemsSource != null) return;
            FillPacientes();
        }

        private void FillPacientes()
        {
            List<dynamic> pacienteDynamicList = GetLista("Paciente");
            List<ConsultorioSagradaFamilia.Models.Paciente> pacienteList = new List<ConsultorioSagradaFamilia.Models.Paciente>();

            foreach (var pacienteDynamic in pacienteDynamicList)
            {
                ConsultorioSagradaFamilia.Models.Paciente paciente = new ConsultorioSagradaFamilia.Models.Paciente
                {
                    Nombre = pacienteDynamic.Nombre,
                    Apellido = pacienteDynamic.Apellido,
                    IdPaciente = pacienteDynamic.IdPaciente
                };

                pacienteList.Add(paciente);
            }

            Paciente.ItemsSource = pacienteList;
            Paciente.DisplayMemberPath = "ApellidoNombre";
            Paciente.SelectedValuePath = "IdPaciente";
        }

        private void Especialidad_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Especialidad.ItemsSource != null) return;
            List<dynamic> especialidadDynamicList = GetLista("Especialidad");
            List<ConsultorioSagradaFamilia.Models.Especialidad> especialidadList = new List<ConsultorioSagradaFamilia.Models.Especialidad>();

            foreach (var especialidadDynamic in especialidadDynamicList)
            {
                ConsultorioSagradaFamilia.Models.Especialidad especialidad = new ConsultorioSagradaFamilia.Models.Especialidad
                {
                    Nombre = especialidadDynamic.Nombre,
                    IdEspecialidad = especialidadDynamic.IdEspecialidad
                };

                especialidadList.Add(especialidad);
            }

            Especialidad.ItemsSource = especialidadList;
            Especialidad.DisplayMemberPath = "Nombre";
            Especialidad.SelectedValuePath = "IdEspecialidad";
        }      

        private void FillMedicos()
        {
            List<dynamic> medicoDynamicList = GetLista("Medico", "idEspecialidad",
                                                        Especialidad.SelectedValue != null ? (int?)Especialidad.SelectedValue : null,
                                                       "idPaciente", null);
                                                       //Paciente.SelectedValue != null ? (int?)Paciente.SelectedValue : null);
            List<ConsultorioSagradaFamilia.Models.Medico> medicoList = new List<ConsultorioSagradaFamilia.Models.Medico>();

            foreach (var medicoDynamic in medicoDynamicList)
            {
                ConsultorioSagradaFamilia.Models.Medico medico = new ConsultorioSagradaFamilia.Models.Medico
                {
                    Nombre = medicoDynamic.Nombre,
                    IdMedico = medicoDynamic.IdMedico,
                    Apellido = medicoDynamic.Apellido
                };

                medicoList.Add(medico);
            }

            Medico.ItemsSource = medicoList;
            Medico.DisplayMemberPath = "ApellidoNombre";
            Medico.SelectedValuePath = "IdMedico";
        }

        private void FillDias()
        {
            if (Medico.SelectedValue == null) return;
            List<dynamic> diasDynamicList = GetLista("DisponibilidadDia", "idMedico",
                                                        (int)Medico.SelectedValue);

            List<string> dias = new List<string>();

            if (diasDynamicList == null) dias.Add("No hay");

            foreach (var diasDynamic in diasDynamicList)
            {
                dias.Add(diasDynamic.ToString());
            }

            Fecha.ItemsSource = dias;
        }

        private void FillHorarios()
        {
            if (Fecha.SelectedValue == null) return;
            string[] fechaStringDia = Fecha.SelectedValue.ToString().Split(' ');
            string[] fechaString = fechaStringDia[1].Split('/');

            DateTime fecha = new DateTime(int.Parse(fechaString[2]), int.Parse(fechaString[1]),
                                          int.Parse(fechaString[0]));
            List<dynamic> horariosDynamicList = GetLista("DisponibilidadHorario", "idMedico",
                                                        (int)Medico.SelectedValue, fechaDesde: fecha);

            List<string> horarios = new List<string>();

            if (horariosDynamicList == null || horariosDynamicList.Count() == 0) horarios.Add("No hay");

            foreach (var horarioDynamic in horariosDynamicList)
            {
                string[] horarioSplit = horarioDynamic.ToString().Split(':');

                if (horarioSplit[0].Count() == 1) horarioSplit[0] = 0 + horarioSplit[0];
                if (horarioSplit[1].Count() == 1) horarioSplit[1] = horarioSplit[1] + 0;

                horarios.Add(string.Join(":", horarioSplit));
            }

            if (TurnoM.IsChecked.GetValueOrDefault())
            {
                if (horarios.First() != "No hay")
                    horarios.RemoveAll(h => int.Parse(h[0] + h[1].ToString()) > 12);
            }
            else if (TurnoT.IsChecked.GetValueOrDefault())
            {
                if (horarios.First() != "No hay")
                    horarios.RemoveAll(h => int.Parse(h[0] + h[1].ToString()) < 12);
            }

            if (horarios.Count == 0) horarios.Add("No hay");

            if (horarioUsadoEnEditTurno != null)
            {
                if (horarios.First() != "No hay")
                {
                    horarios.Add(horarioUsadoEnEditTurno);
                    horarios.Sort();
                }
            }

            Hora.ItemsSource = horarios;
        }

        private void Btm_Crear_Click(object sender, RoutedEventArgs e)
        {
            if (Paciente.SelectedValue == null) { MessageBox.Show("Debe indicar un Paciente"); return; };
            if (Medico.SelectedValue == null) { MessageBox.Show("Debe indicar un Médico"); return; };
            if (Fecha.SelectedValue == null && !TurnoE.IsChecked.GetValueOrDefault())
            { MessageBox.Show("Debe indicar una Fecha"); return; };
            if ((Hora.SelectedValue == null || Hora.SelectedValue.ToString() == "No hay") &&
                !TurnoE.IsChecked.GetValueOrDefault())
            {
                MessageBox.Show("Debe indicar una Hora");
                return;
            }

            DateTime fecha;

            if (TurnoE.IsChecked.GetValueOrDefault())
            {
                fecha = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            }
            else
            {
                string[] arregloFechaConDia = Fecha.SelectedValue.ToString().Split(' ');
                string[] arregloFecha = arregloFechaConDia[1].Split('/');
                string[] arregloHora = Hora.SelectedValue.ToString().Split(':');
                fecha = new DateTime(int.Parse(arregloFecha[2]), int.Parse(arregloFecha[1]),
                                              int.Parse(arregloFecha[0]), int.Parse(arregloHora[0]),
                                              int.Parse(arregloHora[1]), 0);
            }

            ConsultorioSagradaFamilia.Models.Turno turno = new ConsultorioSagradaFamilia.Models.Turno
            {
                IdPaciente = (int)Paciente.SelectedValue,
                IdMedico = (int)Medico.SelectedValue,
                Fecha = fecha,
                Atendido = false
            };

            var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/api");

            var request = new RestRequest("Turno", Method.POST);

            if (idObjetoEditando != 0)
            {
                request = new RestRequest("Turno?id=" + idObjetoEditando, Method.PUT);
                turno.IdTurno = idObjetoEditando;
            }

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Bearer " + DatosUsuario.Token);

            request.AddParameter("undefined",
                "Atendido=false" +
                "&Fecha=" + turno.Fecha.Year + "-" + turno.Fecha.Month + "-" + turno.Fecha.Day + "T" +
                           turno.Fecha.Hour.ToString().PadLeft(2, '0') + "%3A" +
                           turno.Fecha.Minute.ToString().PadLeft(2, '0') + "%3A" +
                           turno.Fecha.Second.ToString().PadLeft(2, '0') + "Z" +
                "&IdMedico=" + turno.IdMedico + "&IdPaciente=" + turno.IdPaciente +
                "&IdTurno=" + turno.IdTurno, ParameterType.RequestBody);

            try
            {
                var response = client.Execute(request);
                string respuesta = response.Content;

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    MessageBox.Show("Turno Creado");
                    if (idObjetoEditando != 0) idObjetoEditando = 0;

                    PacienteMedico pacienteMedico = new PacienteMedico
                    {
                        IdPaciente = turno.IdPaciente,
                        IdMedico = turno.IdMedico
                    };

                    DbContextSingleton.dbContext.GuardarPacienteMedico(pacienteMedico);

                    Turnos turnos = new Turnos();
                    Layout.Frame.Navigate(turnos);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NoContent && idObjetoEditando != 0)
                {
                    MessageBox.Show("Turno Editado");
                    if (idObjetoEditando != 0) idObjetoEditando = 0;

                    PacienteMedico pacienteMedico = new PacienteMedico
                    {
                        IdPaciente = turno.IdPaciente,
                        IdMedico = turno.IdMedico
                    };

                    DbContextSingleton.dbContext.GuardarPacienteMedico(pacienteMedico);

                    Turnos turnos = new Turnos();
                    Layout.Frame.Navigate(turnos);
                }
                else
                {
                    dynamic mensaje = JsonConvert.DeserializeObject(response.Content);
                    if (mensaje != null)
                    {
                        MessageBox.Show(mensaje.Message.ToString());
                    }
                    else
                    {
                        MessageBox.Show("No se pudo conectar al servidor, intente de nuevo mas tarde.");
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("No se pudo conectar al servidor, intente de nuevo mas tarde.");
            }
        }

        private void CargarTurnoParaEditar(int idTurno)
        {
            idObjetoEditando = idTurno;
            dynamic dynamicObj = GetItem("Turno", idTurno);

            ConsultorioSagradaFamilia.Models.Turno turno = new ConsultorioSagradaFamilia.Models.Turno
            {
                IdTurno = dynamicObj.IdTurno,
                Atendido = dynamicObj.Atendido,
                Fecha = dynamicObj.Fecha,
                IdMedico = dynamicObj.IdMedico,
                IdPaciente = dynamicObj.IdPaciente
            };

            FillPacientes();
            Paciente.SelectedValue = turno.IdPaciente;

            FillMedicos();
            Medico.SelectedValue = turno.IdMedico;

            FillDias();

            string dia = string.Empty;

            switch ((int)turno.Fecha.DayOfWeek)
            {
                case 1:
                    dia = "Lunes";
                    break;
                case 2:
                    dia = "Martes";
                    break;
                case 3:
                    dia = "Miércoles";
                    break;
                case 4:
                    dia = "Jueves";
                    break;
                case 5:
                    dia = "Viernes";
                    break;
            }

            Fecha.SelectedValue = dia + " " + turno.Fecha.Day + "/" + turno.Fecha.Month + "/" +
                                  turno.Fecha.Year;


            if (turno.Fecha.Hour == 0) TurnoE.IsChecked = true;
            else if (turno.Fecha.Hour > 12) TurnoT.IsChecked = true;
            else if (turno.Fecha.Hour < 12) TurnoM.IsChecked = true;

            FillHorarios();
            string horarioUsado = turno.Fecha.Hour == 0 ? "" : turno.Fecha.Hour.ToString().PadLeft(2, '0') +
                                 ":" + turno.Fecha.Minute.ToString().PadLeft(2, '0');

            List<string> horarios = (List<string>)Hora.ItemsSource;

            if (horarios == null)
            {
                horarios = new List<string>();
            }

            horarios.Add(horarioUsado);
            horarios.Sort();

            Hora.ItemsSource = horarios;
            Hora.SelectedValue = horarioUsado;
            horarioUsadoEnEditTurno = horarioUsado;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Turnos turnos = new Turnos();
            Layout.Frame.Navigate(turnos);
        }
    }
}
