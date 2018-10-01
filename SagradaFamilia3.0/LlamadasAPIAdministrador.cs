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
        private static string horarioUsadoEnEditTurno = null;
        private static bool editando = false;
        private static int idObjetoEditando = 0;

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
            if(nombre != null)
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
            //if (FormaDePago.ItemsSource != null) return;
            //List<dynamic> formaPagoDynamicList = GetLista("FormaPago");
            //List<FormaPago> formaPagoList = new List<FormaPago>();

            //foreach (var formaPagoDynamic in formaPagoDynamicList)
            //{
            //    FormaPago formaPago = new FormaPago
            //    {
            //        Nombre = formaPagoDynamic.Nombre,
            //        IdFormaPago = formaPagoDynamic.IdFormaPago
            //    };

            //    formaPagoList.Add(formaPago);
            //}

            //FormaDePago.ItemsSource = formaPagoList;
            //FormaDePago.DisplayMemberPath = "Nombre";
            //FormaDePago.SelectedValuePath = "IdFormaPago";
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
            FillPacientes();
        }

        private void TurnoM_Checked()
        {
            if (Fecha.SelectedValue == null) return;
            FillHorarios();
            //string[] fechaStringDia = Fecha.SelectedValue.ToString().Split(' ');
            //string[] fechaString = fechaStringDia[1].Split('/');

            //DateTime fecha = new DateTime(int.Parse(fechaString[2]), int.Parse(fechaString[1]),
            //                              int.Parse(fechaString[0]));
            //List<dynamic> horariosDynamicList = GetLista("DisponibilidadHorario", "idMedico",
            //                                            (int)Medico.SelectedValue, fechaDesde: fecha);

            //List<string> horarios = new List<string>();

            //if (horariosDynamicList == null || horariosDynamicList.Count() == 0) horarios.Add("No hay");

            //foreach (var horarioDynamic in horariosDynamicList)
            //{
            //    string[] horarioSplit = horarioDynamic.ToString().Split(':');

            //    if (horarioSplit[0].Count() == 1) horarioSplit[0] = 0 + horarioSplit[0];
            //    if (horarioSplit[1].Count() == 1) horarioSplit[1] = horarioSplit[1] + 0;

            //    horarios.Add(string.Join(":", horarioSplit));
            //}

            //if (TurnoM.IsChecked.GetValueOrDefault())
            //{
            //    if (horarios.First() != "No hay")
            //        horarios.RemoveAll(h => int.Parse(h[0] + h[1].ToString()) > 12);
            //}
            //else if (TurnoT.IsChecked.GetValueOrDefault())
            //{
            //    if (horarios.First() != "No hay")
            //        horarios.RemoveAll(h => int.Parse(h[0] + h[1].ToString()) < 12);
            //}

            //if (horarios.Count == 0) horarios.Add("No hay");
            //Hora.ItemsSource = horarios;
        }

        private void TurnoT_Checked()
        {
            if (Fecha.SelectedValue == null) return;
            FillHorarios();
            //string[] fechaStringDia = Fecha.SelectedValue.ToString().Split(' ');
            //string[] fechaString = fechaStringDia[1].Split('/');

            //DateTime fecha = new DateTime(int.Parse(fechaString[2]), int.Parse(fechaString[1]),
            //                              int.Parse(fechaString[0]));
            //List<dynamic> horariosDynamicList = GetLista("DisponibilidadHorario", "idMedico",
            //                                            (int)Medico.SelectedValue, fechaDesde: fecha);

            //List<string> horarios = new List<string>();

            //if (horariosDynamicList == null || horariosDynamicList.Count() == 0) horarios.Add("No hay");

            //foreach (var horarioDynamic in horariosDynamicList)
            //{
            //    string[] horarioSplit = horarioDynamic.ToString().Split(':');

            //    if (horarioSplit[0].Count() == 1) horarioSplit[0] = 0 + horarioSplit[0];
            //    if (horarioSplit[1].Count() == 1) horarioSplit[1] = horarioSplit[1] + 0;

            //    horarios.Add(string.Join(":", horarioSplit));
            //}

            //if (TurnoM.IsChecked.GetValueOrDefault())
            //{
            //    if (horarios.First() != "No hay")
            //        horarios.RemoveAll(h => int.Parse(h[0] + h[1].ToString()) > 12);
            //}
            //else if (TurnoT.IsChecked.GetValueOrDefault())
            //{
            //    if (horarios.First() != "No hay")
            //        horarios.RemoveAll(h => int.Parse(h[0] + h[1].ToString()) < 12);
            //}

            //if (horarios.Count == 0) horarios.Add("No hay");
            //Hora.ItemsSource = horarios;
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

                    Turno turno = new Turno
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
                        request = new RestRequest("Turno?id="+idObjetoEditando, Method.PUT);
                        turno.IdTurno = idObjetoEditando;
                    }
                    
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Authorization", "Bearer " + DatosUsuario.Token);

                    request.AddParameter("undefined", 
                        "Atendido=false"+
                        "&Fecha="+ turno.Fecha.Year + "-" + turno.Fecha.Month + "-" + turno.Fecha.Day + "T" +
                                   turno.Fecha.Hour.ToString().PadLeft(2,'0') + "%3A" + 
                                   turno.Fecha.Minute.ToString().PadLeft(2, '0') + "%3A" + 
                                   turno.Fecha.Second.ToString().PadLeft(2, '0') + "Z" +
                        "&IdMedico=" + turno.IdMedico + "&IdPaciente=" + turno.IdPaciente + 
                        "&IdTurno=" + turno.IdTurno, ParameterType.RequestBody);

                    try
                    {
                        var response = client.Execute(request);
                        string respuesta = response.Content;

                        if(response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            MessageBox.Show("Turno Creado");
                            if (idObjetoEditando != 0) idObjetoEditando = 0;
                            limpiarTurno();
                            
                        }
                        else if(response.StatusCode == System.Net.HttpStatusCode.NoContent && idObjetoEditando != 0)
                        {
                            MessageBox.Show("Turno Editado");
                            if (idObjetoEditando != 0) idObjetoEditando = 0;
                            limpiarTurno();
                        }
                        else
                        {
                            dynamic mensaje = JsonConvert.DeserializeObject(response.Content);
                            if(mensaje != null)
                            {
                                MessageBox.Show(mensaje.Message.ToString());
                            }
                            else
                            {
                                MessageBox.Show("No se pudo conectar al servidor, intente de nuevo mas tarde.");
                            }
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

        private void butBuscar_Click(object sender, RoutedEventArgs e)
        {
            DateTime? fechaDesde = null;
            DateTime? fechaHasta = null;
            int? idMedico = null;
            int? idPaciente = null;
            double totalWidth = 0;
            string nombre = string.Empty;
            string apellido = string.Empty;

            switch (mostrando)
            {
                case 1:
                    nombre = busqNombreBox.Text == string.Empty ? null : busqNombreBox.Text;
                    apellido = busqApellidoBox.Text == string.Empty ? null : busqApellidoBox.Text;

                    List<dynamic> medicosDynamicList = GetLista("Medico", nombre: nombre, apellido: apellido);

                    List<ConsultorioSagradaFamilia.Models.Medico> medicos = new List<ConsultorioSagradaFamilia.Models.Medico>();

                    foreach(var medicoDynamic in medicosDynamicList)
                    {
                        ConsultorioSagradaFamilia.Models.Medico medico = new ConsultorioSagradaFamilia.Models.Medico
                        {
                            Apellido = medicoDynamic.Apellido,
                            DNI = medicoDynamic.DNI,
                            CUIL = medicoDynamic.CUIL,
                            IdMedico = medicoDynamic.IdMedico,
                            Matricula = medicoDynamic.Matricula,
                            Monto = medicoDynamic.Monto,
                            Nombre = medicoDynamic.Nombre
                        };

                        medicos.Add(medico);                        
                    }

                    lista.IsReadOnly = true;
                    lista.SelectionMode = DataGridSelectionMode.Single;

                    totalWidth = lista.ActualWidth;
                    lista.ItemsSource = medicos;
                    lista.Columns[1].Width = totalWidth / 5;
                    lista.Columns[4].Width = totalWidth / 5;
                    lista.Columns[5].Width = totalWidth / 5;
                    lista.Columns[6].Width = totalWidth / 5;
                    lista.Columns[7].Width = totalWidth / 5;
                    lista.Columns[0].Visibility = Visibility.Collapsed;
                    lista.Columns[2].Visibility = Visibility.Collapsed;
                    lista.Columns[3].Visibility = Visibility.Collapsed;
                    lista.Columns[4].Header = "Apellido y Nombre";
                    break;
                case 2:
                    nombre = busqNombreBox.Text == string.Empty ? null : busqNombreBox.Text;
                    apellido = busqApellidoBox.Text == string.Empty ? null : busqApellidoBox.Text;

                    List<dynamic> pacientesDynamicList = GetLista("Paciente", nombre: nombre, apellido: apellido);

                    List<Paciente> pacientes = new List<Paciente>();

                    foreach (var pacienteDynamic in pacientesDynamicList)
                    {
                        Paciente paciente = new Paciente
                        {
                            Apellido = pacienteDynamic.Apellido,
                            DNI = pacienteDynamic.DNI,
                            IdPaciente = pacienteDynamic.IdPaciente,
                            Nombre = pacienteDynamic.Nombre,
                            Direccion = pacienteDynamic.Direccion,
                            FechaNacimiento = pacienteDynamic.FechaNacimiento
                        };

                        pacientes.Add(paciente);
                    }

                    lista.IsReadOnly = true;
                    lista.SelectionMode = DataGridSelectionMode.Single;

                    totalWidth = lista.ActualWidth;
                    lista.ItemsSource = pacientes;
                    lista.Columns[1].Width = totalWidth / 4;
                    lista.Columns[4].Width = totalWidth / 4;
                    lista.Columns[6].Width = totalWidth / 4;
                    lista.Columns[7].Width = totalWidth / 4;
                    lista.Columns[0].Visibility = Visibility.Collapsed;
                    lista.Columns[5].Visibility = Visibility.Collapsed;
                    lista.Columns[2].Visibility = Visibility.Collapsed;
                    lista.Columns[3].Visibility = Visibility.Collapsed;
                    lista.Columns[4].Header = "Apellido y Nombre";
                    lista.Columns[7].Header = "Fecha de Nacimiento";
                    break;
                case 3:
                    
                    if(dateBusqDesde.SelectedDate != null)
                    {
                        fechaDesde = dateBusqDesde.SelectedDate;
                    }
                    if (dateBusqHasta.SelectedDate != null)
                    {
                        fechaHasta = dateBusqHasta.SelectedDate;
                    }
                    if(combBuscMedico.SelectedValue != null)
                    {
                        idMedico = (int) combBuscMedico.SelectedValue;
                        if (idMedico == 0) idMedico = null;
                    }
                    if (combBuscPaciente.SelectedValue != null)
                    {
                        idPaciente = (int)combBuscPaciente.SelectedValue;
                        if (idPaciente == 0) idPaciente = null;
                    }

                    List<dynamic> turnosDynamicList = GetLista("TurnosPorPaciente","idMedico", idMedico, "idPaciente",
                                                                idPaciente,fechaDesde,fechaHasta);

                    List<TurnosPorPaciente> turnos = new List<TurnosPorPaciente>();

                    foreach (var turnodynamic in turnosDynamicList)
                    {
                        string[] fechaSplit = turnodynamic.FechaString.ToString().Split('-');
                        string fechaReal = fechaSplit[2] + "/" + fechaSplit[1] + "/" + fechaSplit[0];

                        string[] horaSplit = turnodynamic.HoraString.ToString().Split('.');
                        string[] horaSplit2 = horaSplit[0].ToString().Split(':');
                        string horaReal = horaSplit2[0] + ":" + horaSplit2[1];

                        TurnosPorPaciente turno = new TurnosPorPaciente
                        {
                            CUILMedico = turnodynamic.CUILMedico,
                            DNIMedico = turnodynamic.DNIMedico,
                            Fecha = turnodynamic.Fecha,
                            FechaString = fechaReal,
                            HoraString = horaReal != "00:00" ? horaReal : "En espera",
                            IdMedico = turnodynamic.IdMedico,
                            IdPaciente = turnodynamic.IdPaciente,
                            MatriculaMedico = turnodynamic.MatriculaMedico,
                            NombreMedico = turnodynamic.NombreMedico,
                            NombrePaciente = turnodynamic.NombrePaciente,
                            Atendido = turnodynamic.Atendido,
                            IdTurno = turnodynamic.IdTurno
                        };

                        turnos.Add(turno);
                    }

                    lista.IsReadOnly = true;
                    lista.SelectionMode = DataGridSelectionMode.Single;

                    totalWidth = lista.ActualWidth; 
                    lista.ItemsSource = turnos;
                    lista.Columns[1].Width = totalWidth / 5;
                    lista.Columns[4].Width = totalWidth / 5;
                    lista.Columns[8].Width = totalWidth / 5;
                    lista.Columns[9].Width = totalWidth / 5;
                    lista.Columns[11].Width = totalWidth / 5;
                    lista.Columns[1].Header = "Paciente";                    
                    lista.Columns[4].Header = "Médico";
                    lista.Columns[8].Header = "Fecha";
                    lista.Columns[9].Header = "Hora";
                    lista.Columns[11].Header = "Atendido";
                    lista.Columns[0].Visibility = Visibility.Collapsed;
                    lista.Columns[2].Visibility = Visibility.Collapsed;
                    lista.Columns[3].Visibility = Visibility.Collapsed;
                    lista.Columns[5].Visibility = Visibility.Collapsed;
                    lista.Columns[6].Visibility = Visibility.Collapsed;
                    lista.Columns[7].Visibility = Visibility.Collapsed;
                    lista.Columns[10].Visibility = Visibility.Collapsed;

                    break;
                case 4:
                    //cargarCrearPago();
                    break;
            }
            
        }

        private void combBuscMedico_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (combBuscMedico.ItemsSource != null) return;
            List<dynamic> medicoDynamicList = GetLista("Medico");
            List<ConsultorioSagradaFamilia.Models.Medico> medicoList = new List<ConsultorioSagradaFamilia.Models.Medico>();

            ConsultorioSagradaFamilia.Models.Medico medicoNulo = new ConsultorioSagradaFamilia.Models.Medico();
            medicoNulo.Nombre = "[Ninguno]";

            medicoList.Add(medicoNulo);

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
            
            combBuscMedico.ItemsSource = medicoList;
            combBuscMedico.DisplayMemberPath = "ApellidoNombre";
            combBuscMedico.SelectedValuePath = "IdMedico";
        }

        private void combBuscPaciente_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (combBuscPaciente.ItemsSource != null) return;
            List<dynamic> pacienteDynamicList = GetLista("Paciente");
            List<Paciente> pacienteList = new List<Paciente>();

            Paciente pacienteNulo = new Paciente();
            pacienteNulo.Nombre = "[Ninguno]";

            pacienteList.Add(pacienteNulo);

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

            

            combBuscPaciente.ItemsSource = pacienteList;
            combBuscPaciente.DisplayMemberPath = "ApellidoNombre";
            combBuscPaciente.SelectedValuePath = "IdPaciente";
        }
        
        private void CargarTurnoParaEditar(int idTurno)
        {
            idObjetoEditando = idTurno;
            dynamic dynamicObj = GetItem("Turno", idTurno);

            Turno turno = new Turno
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
            horarios.Add(horarioUsado);
            horarios.Sort();

            Hora.ItemsSource = horarios;
            Hora.SelectedValue = horarioUsado;
            horarioUsadoEnEditTurno = horarioUsado;
        }

        private void FillPacientes()
        {
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
        
        private void FillMedicos()
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
                if(horarios.First() != "No hay")
                {
                    horarios.Add(horarioUsadoEnEditTurno);
                    horarios.Sort();
                }                               
            }            

            Hora.ItemsSource = horarios;
        }
        
        //Se utiliza cuando estoy editando y salgo sin guardar mi edicion, entonces no se guardan en
        //la vista los campos rellenados y se les quitan todos los datos cargados
        public void LimpiarCamposSiEstabaEditando()
        {
            if (editando == true)
            {
                switch (mostrando)
                {
                    case 1:
                        limpiarMedico();
                        break;
                    case 2:
                        limpiarPaciente();
                        break;
                    case 3:
                        limpiarTurno();
                        Paciente.ItemsSource = null;
                        Medico.ItemsSource = null;
                        TurnoM.IsChecked = false;
                        TurnoT.IsChecked = false;
                        TurnoE.IsChecked = false;
                        combObras.ItemsSource = null;
                        Hora.ItemsSource = null;
                        Especialidad.ItemsSource = null;
                        Fecha.ItemsSource = null;
                        idObjetoEditando = 0;
                        break;
                    case 4:
                        limpiarPago();
                        break;
                }

            }
            editando = false;
        }

        public void EliminarTurno(int idTurno)
        {
            var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/api");

            var request = new RestRequest("Turno?id=" + idTurno, Method.DELETE);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Bearer " + DatosUsuario.Token);

            try
            {
                var response = client.Execute(request);
                string respuesta = response.Content;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Turno Borrado");
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
    }
}
