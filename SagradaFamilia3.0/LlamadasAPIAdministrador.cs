using ConsultorioSagradaFamilia.Models;
using MahApps.Metro.Controls;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        fechaDesde.GetValueOrDefault().ToShortDateString();
                }
                else
                {
                    nombreControlador = nombreControlador + "fechaDesde=" + 
                        fechaDesde.GetValueOrDefault().ToShortDateString();
                }
            }
            if (fechaHasta != null)
            {
                if (nombreControlador.Last() != '?')
                {
                    nombreControlador = nombreControlador + "&fechaHasta=" +
                        fechaHasta.GetValueOrDefault().ToShortDateString();
                }
                else
                {
                    nombreControlador = nombreControlador + "fechaHasta=" +
                        fechaHasta.GetValueOrDefault().ToShortDateString();
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
            List<dynamic> medicoDynamicList = GetLista("Medico", "idEspecialidad", (int)Especialidad.SelectedValue);
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
            List<dynamic> horariosDynamicList = GetLista("DisponibilidadHorario", "idMedico",
                                                        (int)Medico.SelectedValue, fechaDesde: DateTime.Today);

            List<string> horarios = new List<string>();

            foreach (var horarioDynamic in horariosDynamicList)
            {
                horarios.Add(horarioDynamic);
            }

            Hora.ItemsSource = horarios;
        }

        private void Fecha_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            //if (Fecha.ItemsSource != null) return;
            //List<string> fechas = new List<string>();

            //for(int i = 0; i < 14; i++)
            //{
            //    DateTime fecha = DateTime.Now.AddDays(i);
            //    if (fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday) continue;
            //    string fechaString = string.Empty;
            //    switch (fecha.DayOfWeek)
            //    {
            //        case DayOfWeek.Monday:
            //            fechaString = "Lunes";
            //            break;
            //        case DayOfWeek.Tuesday:
            //            fechaString = "Martes";
            //            break;
            //        case DayOfWeek.Wednesday:
            //            fechaString = "Miércoles";
            //            break;
            //        case DayOfWeek.Thursday:
            //            fechaString = "Jueves";
            //            break;
            //        case DayOfWeek.Friday:
            //            fechaString = "Viernes";
            //            break;
            //    }
            //    fechaString = fechaString + " " + fecha.Day + "/" + fecha.Month + "/" + fecha.Year + " ";

            //    fechas.Add(fechaString);
            //}

            //Fecha.ItemsSource = fechas;
        }

        private void Hora_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            List<dynamic> horariosDynamicList = GetLista("DisponibilidadHorario", "idMedico",
                                                        (int)Medico.SelectedValue, fechaDesde: new DateTime(2018,10,1));
            
            List<string> horarios = new List<string>();

            if (horariosDynamicList == null) horarios.Add("No hay");

            foreach (var horarioDynamic in horariosDynamicList)
            {
                string[] horarioSplit = horarioDynamic.ToString().Split(':');

                if (horarioSplit[0].Count() == 1) horarioSplit[0] = 0 + horarioSplit[0];
                if (horarioSplit[1].Count() == 1) horarioSplit[1] = horarioSplit[1] + 0;

                horarios.Add(string.Join(":", horarioSplit));
            }

            Hora.ItemsSource = horarios;
            //8.00 hs a 12.00 hs y de 16.00 hs a 20.30 hs.
            //List<string> horarios = new List<string>();
            //if (TurnoM.IsChecked.GetValueOrDefault())
            //{
            //    TimeSpan timeSpan = new TimeSpan(8, 0, 0);
            //    for (int i=0; i < 12; i++)
            //    {
            //        string horario = timeSpan.Hours + ":" + timeSpan.Minutes;
            //        horarios.Add(horario);

            //        timeSpan = timeSpan.Add(new TimeSpan(0, 20, 0));
            //    }
            //}
            //else
            //{
            //    TimeSpan timeSpan = new TimeSpan(16, 0, 0);
            //    for (int i = 0; i < 12; i++)
            //    {
            //        string horario = timeSpan.Hours + ":" + timeSpan.Minutes;
            //        horarios.Add(horario);

            //        timeSpan = timeSpan.Add(new TimeSpan(0, 20, 0));
            //    }
            //}
            //Hora.ItemsSource = horarios;
        }
    }
}
