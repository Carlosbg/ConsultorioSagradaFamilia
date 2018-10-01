using ConsultorioSagradaFamilia.Models;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SagradaFamilia3._0
{
    public partial class Administrador : MetroWindow
    {
        private dynamic GetLista(string nombreControlador, string id1Nombre = null, int? id1 = null,
                                 string id2Nombre = null, int? id2 = null, DateTime? fechaDesde = null,
                                 DateTime? fechaHasta = null)
        {
            nombreControlador = nombreControlador + "?";

            if (id1Nombre != null) nombreControlador = nombreControlador + id1Nombre + "=" + id1;
            if (id2Nombre != null) {
                if(nombreControlador.Last() != '?')
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
                catch(Exception e)
                {
                    dynamicObj = JToken.ToString(); 
                }

                lista.Add(dynamicObj);
            }

            return lista;
        }

        private void FormaDePago_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (FormaDePago.ItemsSource != null) return;
            List<dynamic> formaPagoDynamicList = GetLista("FormaPago");
            List<FormaPago> formaPagoList = new List<FormaPago>();

            foreach (var formaPagoDynamic in formaPagoDynamicList)
            {
                FormaPago formaPago = new FormaPago
                {
                    Nombre = formaPagoDynamic.Nombre,
                    IdFormaPago = formaPagoDynamic.IdFormaPago
                };

                formaPagoList.Add(formaPago);
            }

            FormaDePago.ItemsSource = formaPagoList;
            FormaDePago.DisplayMemberPath = "Nombre";
            FormaDePago.SelectedValuePath = "IdFormaPago";
        }

        private void FormaDePago_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //if (((FormaPago)FormaDePago.SelectedItem).Nombre == "Obra Social")
            //{
            //    List<dynamic> obrasSocialesDynamicList = GetLista("ObraSocial");
            //    List<ObraSocial> obrasSocialesList = new List<ObraSocial>();

            //    foreach (var obraSocialDynamic in obrasSocialesDynamicList)
            //    {
            //        ObraSocial obraSocial = new ObraSocial
            //        {
            //            Nombre = obraSocialDynamic.Nombre,
            //            IdObraSocial = obraSocialDynamic.IdObraSocial
            //        };

            //        obrasSocialesList.Add(obraSocial);
            //    }

            //    BancObra.ItemsSource = obrasSocialesList;
            //    BancObra.DisplayMemberPath = "Nombre";
            //    BancObra.SelectedValuePath = "IdObraSocial";
            //}
        }

        private void Especialidad_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Especialidad.ItemsSource != null) return;
            List<dynamic> especialidadDynamicList = GetLista("Especialidad");
            List<Especialidad> especialidadList = new List<Especialidad>();

            foreach (var especialidadDynamic in especialidadDynamicList)
            {
                Especialidad especialidad = new Especialidad
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

        private void Especialidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<dynamic> medicoDynamicList = GetLista("Medico", "idEspecialidad",
                                                        Especialidad.SelectedValue != null ? (int?)Especialidad.SelectedValue : null,
                                                       "idPaciente",
                                                       Paciente.SelectedValue != null ? (int?)Paciente.SelectedValue : null);
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

        private void Medico_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void Fecha_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                if(horarios.First() != "No hay")
                    horarios.RemoveAll(h => int.Parse(h[0] + h[1].ToString()) > 12);
            }
            else if (TurnoT.IsChecked.GetValueOrDefault())
            {
                if (horarios.First() != "No hay")
                    horarios.RemoveAll(h => int.Parse(h[0] + h[1].ToString()) < 12);
            }

            if (horarios.Count == 0) horarios.Add("No hay");
            Hora.ItemsSource = horarios;            
        }

        private void Hora_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
                     
        }

        private void Paciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<dynamic> medicoDynamicList = GetLista("Medico", "idEspecialidad", 
                                                        Especialidad.SelectedValue != null ? (int?)Especialidad.SelectedValue : null,
                                                       "idPaciente",
                                                       Paciente.SelectedValue != null ? (int?)Paciente.SelectedValue : null);
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
            List<dynamic> pacienteDynamicList = GetLista("Paciente");
            List<Paciente> pacienteList = new List<Paciente>();

            foreach (var pacienteDynamic in pacienteDynamicList)
            {
                Paciente paciente = new Paciente
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

        private void TurnoM_Checked()
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
            Hora.ItemsSource = horarios;
        }

        private void TurnoT_Checked()
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
            Hora.ItemsSource = horarios;
        }

        private void butGuardar_Click(object sender, RoutedEventArgs e)
        {
            switch (mostrando)
            {
                case 1:
                    //cargarCrearMedico();
                    break;
                case 2:
                    //cargarCrearPaciente();
                    break;
                case 3:

                    if (Paciente.SelectedValue == null) { MessageBox.Show("Debe indicar un Paciente"); return; };
                    if (Medico.SelectedValue == null) { MessageBox.Show("Debe indicar un Médico"); return; };
                    if (Fecha.SelectedValue == null) { MessageBox.Show("Debe indicar una Fecha"); return; };
                    if (Hora.SelectedValue == null || Hora.SelectedValue.ToString() == "No hay")
                    {
                        MessageBox.Show("Debe indicar una Hora");
                        return;
                    }

                    string[] arregloFechaConDia = Fecha.SelectedValue.ToString().Split(' ');
                    string[] arregloFecha = arregloFechaConDia[1].Split('/');
                    string[] arregloHora = Hora.SelectedValue.ToString().Split(':');
                    DateTime fecha = new DateTime(int.Parse(arregloFecha[2]), int.Parse(arregloFecha[1]),
                                                  int.Parse(arregloFecha[0]), int.Parse(arregloHora[0]), 
                                                  int.Parse(arregloHora[1]), 0);
                    Turno turno = new Turno
                    {
                        IdPaciente = (int)Paciente.SelectedValue,
                        IdMedico = (int)Medico.SelectedValue,
                        Fecha = fecha,
                        Atendido = false
                    };

                    var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/api");
                    var request = new RestRequest("Turno", Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Authorization", "Bearer " + DatosUsuario.Token);

                    request.AddParameter("undefined", 
                        "Atendido=false"+
                        "&Fecha="+ turno.Fecha.Year + "-" + turno.Fecha.Month + "-" + turno.Fecha.Day + "T" +
                                   turno.Fecha.Hour.ToString().PadLeft(2,'0') + "%3A" + 
                                   turno.Fecha.Minute.ToString().PadLeft(2, '0') + "%3A" + 
                                   turno.Fecha.Second.ToString().PadLeft(2, '0') + "Z" +
                        "&IdMedico=" + turno.IdMedico + "&IdPaciente=" + turno.IdPaciente + 
                        "&IdTurno=0", ParameterType.RequestBody);

                    try
                    {
                        var response = client.Execute(request);
                        string respuesta = response.Content;

                        if(response.StatusCode != System.Net.HttpStatusCode.Created)
                        {
                            dynamic mensaje = JsonConvert.DeserializeObject(response.Content);
                            MessageBox.Show(mensaje.Message.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Turno Creado");
                        }
                    }
                    catch(Exception x)
                    {
                        MessageBox.Show("No se pudo conectar al servidor, intente de nuevo mas tarde.");
                    }                   

                    break;
                case 4:
                    //cargarCrearPago();
                    break;

            }

        }
    }
}
