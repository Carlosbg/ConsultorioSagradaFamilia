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

            var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/api");
            var request = new RestRequest(nombreControlador, Method.GET);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Bearer " + DatosUsuario.Token);

            var response = client.Execute(request);
            var content = response.Content;

            JArray rss = JArray.Parse(content);

            var lista = new List<dynamic>();

            foreach (var JToken in rss)
            {
                dynamic dynamicObj = JObject.Parse(JToken.ToString());

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
            if (((FormaPago)FormaDePago.SelectedItem).Nombre == "Obra Social")
            {
                List<dynamic> obrasSocialesDynamicList = GetLista("ObraSocial");
                List<ObraSocial> obrasSocialesList = new List<ObraSocial>();

                foreach (var obraSocialDynamic in obrasSocialesDynamicList)
                {
                    ObraSocial obraSocial = new ObraSocial
                    {
                        Nombre = obraSocialDynamic.Nombre,
                        IdObraSocial = obraSocialDynamic.IdObraSocial
                    };

                    obrasSocialesList.Add(obraSocial);
                }

                BancObra.ItemsSource = obrasSocialesList;
                BancObra.DisplayMemberPath = "Nombre";
                BancObra.SelectedValuePath = "IdObraSocial";
            }
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
    }
}
