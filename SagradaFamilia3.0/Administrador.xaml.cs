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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Net.Http;
using System.Net;
using System.IO;
using RestSharp;
using SimpleJson;
using Newtonsoft.Json.Linq;
using ConsultorioSagradaFamilia.Models;

namespace SagradaFamilia3._0
{
    /// <summary>
    /// Lógica de interacción para Administrador.xaml
    /// </summary>
    public partial class Administrador : MetroWindow
    {
        private int mostrando = 0;
        //mostrando = 1: medico
        //mostrando = 2: paciente
        //mostrando = 3: turno
        //mostrando = 4: pago

        public Administrador()
        {
            InitializeComponent();
            limpiarPantalla();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
               
        private void cargarCrearTurno() {
            limpiarPantalla();

            butGuardar.IsEnabled = true;
            butGuardar.Visibility = Visibility.Visible;

            butLimpiar.IsEnabled = true;
            butLimpiar.Visibility = Visibility.Visible;

            labId.IsEnabled = true;
            labId.Visibility = Visibility.Visible;

            fdp.IsEnabled = true;
            fdp.Visibility = Visibility.Visible;

            banOb.IsEnabled = true;
            banOb.Visibility = Visibility.Visible;

            esp.IsEnabled = true;
            esp.Visibility = Visibility.Visible;

            fech.IsEnabled = true;
            fech.Visibility = Visibility.Visible;

            hor.IsEnabled = true;
            hor.Visibility = Visibility.Visible;

            med.IsEnabled = true;
            med.Visibility = Visibility.Visible;

            FormaDePago.IsEnabled = true;
            FormaDePago.Visibility = Visibility.Visible;

            BancObra.IsEnabled = true;
            BancObra.Visibility = Visibility.Visible;

            Especialidad.IsEnabled = true;
            Especialidad.Visibility = Visibility.Visible;

            Fecha.IsEnabled = true;
            Fecha.Visibility = Visibility.Visible;

            Medico.IsEnabled = true;
            Medico.Visibility = Visibility.Visible;

            Hora.IsEnabled = true;
            Hora.Visibility = Visibility.Visible;

            TurnoM.IsEnabled = true;
            TurnoM.Visibility = Visibility.Visible;

            TurnoT.IsEnabled = true;
            TurnoT.Visibility = Visibility.Visible;
        }

        private void cargarGenerarInforme() {
            limpiarPantalla();

            Medic.IsEnabled = true;
            Medic.Visibility = Visibility.Visible;
            Medic.IsChecked = false;

            DirMedic.IsEnabled = true;
            DirMedic.Visibility = Visibility.Visible;
            DirMedic.IsChecked = false;

            dPer.IsEnabled = true;
            dPer.Visibility = Visibility.Visible;

            desde.IsEnabled = true;
            desde.Visibility = Visibility.Visible;

            sta.IsEnabled = true;
            sta.Visibility = Visibility.Visible;

            hasta.IsEnabled = true;
            hasta.Visibility = Visibility.Visible;

        }
        
        private void limpiarPantalla() {

            labEsp.IsEnabled = false;
            labEsp.Visibility = Visibility.Hidden;

            datEsp.IsEnabled = false;
            datEsp.Visibility = Visibility.Hidden;

            butAgrEsp.IsEnabled = false;
            butAgrEsp.Visibility = Visibility.Hidden;

            butQuitEsp.IsEnabled = false;
            butQuitEsp.Visibility = Visibility.Hidden;

            combEsp.IsEnabled = false;
            combEsp.Visibility = Visibility.Hidden;

            butEdit.IsEnabled = false;
            butEdit.Visibility = Visibility.Hidden;

            butGuardar.IsEnabled = false;
            butGuardar.Visibility = Visibility.Hidden;

            butLimpiar.IsEnabled = false;
            butLimpiar.Visibility = Visibility.Hidden;

            lista.IsEnabled = false;
            lista.Visibility = Visibility.Hidden;

            butNuevo.IsEnabled = false;
            butNuevo.Visibility = Visibility.Hidden;

            butBuscar.IsEnabled = false;
            butBuscar.Visibility = Visibility.Hidden;

            butElim.IsEnabled = false;
            butElim.Visibility = Visibility.Hidden;

            turn.IsEnabled = false;
            turn.Visibility = Visibility.Hidden;

            Medic.IsEnabled = false;
            Medic.Visibility = Visibility.Hidden;

            DirMedic.IsEnabled = false;
            DirMedic.Visibility = Visibility.Hidden;

            fdp.IsEnabled = false;
            fdp.Visibility = Visibility.Hidden;

            banOb.IsEnabled = false;
            banOb.Visibility = Visibility.Hidden;

            esp.IsEnabled = false;
            esp.Visibility = Visibility.Hidden;

            fech.IsEnabled = false;
            fech.Visibility = Visibility.Hidden;

            hor.IsEnabled = false;
            hor.Visibility = Visibility.Hidden;

            med.IsEnabled = false;
            med.Visibility = Visibility.Hidden;

            labId.IsEnabled = false;
            labId.Visibility = Visibility.Hidden;

            FormaDePago.IsEnabled = false;
            FormaDePago.Visibility = Visibility.Hidden;

            BancObra.IsEnabled = false;
            BancObra.Visibility = Visibility.Hidden;

            Especialidad.IsEnabled = false;
            Especialidad.Visibility = Visibility.Hidden;

            Fecha.IsEnabled = false;
            Fecha.Visibility = Visibility.Hidden;

            Medico.IsEnabled = false;
            Medico.Visibility = Visibility.Hidden;

            Hora.IsEnabled = false;
            Hora.Visibility = Visibility.Hidden;
                        
            TurnoM.IsEnabled = false;
            TurnoM.Visibility = Visibility.Hidden;

            TurnoT.IsEnabled = false;
            TurnoT.Visibility = Visibility.Hidden;

            Medics.IsEnabled = false;
            Medics.Visibility = Visibility.Hidden;

            dPer.IsEnabled = false;
            dPer.Visibility = Visibility.Hidden;

            desde.IsEnabled = false;
            desde.Visibility = Visibility.Hidden;

            sta.IsEnabled = false;
            sta.Visibility = Visibility.Hidden;

            hasta.IsEnabled = false;
            hasta.Visibility = Visibility.Hidden;

            Pagos.IsEnabled = false;
            Pagos.Visibility = Visibility.Hidden;

            PacientesPorObra.IsEnabled = false;
            PacientesPorObra.Visibility = Visibility.Hidden;

            ProximosTurnos.IsEnabled = false;
            ProximosTurnos.Visibility = Visibility.Hidden;

            PacientesEnEspera.IsEnabled = false;
            PacientesEnEspera.Visibility = Visibility.Hidden;

            PacientesAtendidos.IsEnabled = false;
            PacientesAtendidos.Visibility = Visibility.Hidden;

            ListadoDePagos.IsEnabled = false;
            ListadoDePagos.Visibility = Visibility.Hidden;

            ListadoDePacientesObra.IsEnabled = false;
            ListadoDePacientesObra.Visibility = Visibility.Hidden;

            ListadoDePacientesMedico.IsEnabled = false;
            ListadoDePacientesMedico.Visibility = Visibility.Hidden;

            ListadoDeTurnos.IsEnabled = false;
            ListadoDeTurnos.Visibility = Visibility.Hidden;

            labNom.IsEnabled = false;
            labNom.Visibility = Visibility.Hidden;

            labAp.IsEnabled = false;
            labAp.Visibility = Visibility.Hidden;

            labDni.IsEnabled = false;
            labDni.Visibility = Visibility.Hidden;

            labNat.IsEnabled = false;
            labNat.Visibility = Visibility.Hidden;

            nameBox.IsEnabled = false;
            nameBox.Visibility = Visibility.Hidden;

            apBox.IsEnabled = false;
            apBox.Visibility = Visibility.Hidden;

            dniBox.IsEnabled = false;
            dniBox.Visibility = Visibility.Hidden;

            natBox.IsEnabled = false;
            natBox.Visibility = Visibility.Hidden;

            labDir.IsEnabled = false;
            labDir.Visibility = Visibility.Hidden;

            labTel.IsEnabled = false;
            labTel.Visibility = Visibility.Hidden;

            labCel.IsEnabled = false;
            labCel.Visibility = Visibility.Hidden;

            labEm.IsEnabled = false;
            labEm.Visibility = Visibility.Hidden;

            dirBox.IsEnabled = false;
            dirBox.Visibility = Visibility.Hidden;

            telBox.IsEnabled = false;
            telBox.Visibility = Visibility.Hidden;

            celBox.IsEnabled = false;
            celBox.Visibility = Visibility.Hidden;

            emBox.IsEnabled = false;
            emBox.Visibility = Visibility.Hidden;

            labFormas.IsEnabled = false;
            labFormas.Visibility = Visibility.Hidden;

            checkEfec.IsEnabled = false;
            checkEfec.Visibility = Visibility.Hidden;

            checkDep.IsEnabled = false;
            checkDep.Visibility = Visibility.Hidden;

            checkObra.IsEnabled = false;
            checkObra.Visibility = Visibility.Hidden;

            datObras.IsEnabled = false;
            datObras.Visibility = Visibility.Hidden;

            combObras.IsEnabled = false;
            combObras.Visibility = Visibility.Hidden;

            butAgr.IsEnabled = false;
            butAgr.Visibility = Visibility.Hidden;

            butQuit.IsEnabled = false;
            butQuit.Visibility = Visibility.Hidden;

            labHorarios.IsEnabled = false;
            labHorarios.Visibility = Visibility.Hidden;

            checkLunM.IsEnabled = false;
            checkLunM.Visibility = Visibility.Hidden;

            checkMarM.IsEnabled = false;
            checkMarM.Visibility = Visibility.Hidden;

            checkMierM.IsEnabled = false;
            checkMierM.Visibility = Visibility.Hidden;

            checkJueM.IsEnabled = false;
            checkJueM.Visibility = Visibility.Hidden;

            checkVierM.IsEnabled = false;
            checkVierM.Visibility = Visibility.Hidden;

            checkLunT.IsEnabled = false;
            checkLunT.Visibility = Visibility.Hidden;

            checkMarT.IsEnabled = false;
            checkMarT.Visibility = Visibility.Hidden;

            checkMierT.IsEnabled = false;
            checkMierT.Visibility = Visibility.Hidden;

            checkJueT.IsEnabled = false;
            checkJueT.Visibility = Visibility.Hidden;

            checkVierT.IsEnabled = false;
            checkVierT.Visibility = Visibility.Hidden;

            labMan.IsEnabled = false;
            labMan.Visibility = Visibility.Hidden;

            labTar.IsEnabled = false;
            labTar.Visibility = Visibility.Hidden;

            butCrear.IsEnabled = false;
            butCrear.Visibility = Visibility.Hidden;

            butEditar.IsEnabled = false;
            butEditar.Visibility = Visibility.Hidden;

            butEliminar.IsEnabled = false;
            butEliminar.Visibility = Visibility.Hidden;

            gridListado.IsEnabled = false;
            gridListado.Visibility = Visibility.Hidden;

        }

        private void generarInforme_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();

            cargarGenerarInforme();
        }

        private void Medic_Checked(object sender, RoutedEventArgs e)
        {             

            Medics.IsEnabled = true;
            Medics.Visibility = Visibility.Visible;

            dPer.IsEnabled = true;
            dPer.Visibility = Visibility.Visible;

            desde.IsEnabled = true;
            desde.Visibility = Visibility.Visible;

            sta.IsEnabled = true;
            sta.Visibility = Visibility.Visible;

            hasta.IsEnabled = true;
            hasta.Visibility = Visibility.Visible;

            Pagos.IsEnabled = true;
            Pagos.Visibility = Visibility.Visible;

            PacientesPorObra.IsEnabled = true;
            PacientesPorObra.Visibility = Visibility.Visible;

            ProximosTurnos.IsEnabled = true;
            ProximosTurnos.Visibility = Visibility.Visible;

            PacientesEnEspera.IsEnabled = true;
            PacientesEnEspera.Visibility = Visibility.Visible;

            PacientesAtendidos.IsEnabled = true;
            PacientesAtendidos.Visibility = Visibility.Visible;

            ListadoDePagos.IsEnabled = false;
            ListadoDePagos.Visibility = Visibility.Hidden;

            ListadoDePacientesObra.IsEnabled = false;
            ListadoDePacientesObra.Visibility = Visibility.Hidden;

            ListadoDePacientesMedico.IsEnabled = false;
            ListadoDePacientesMedico.Visibility = Visibility.Hidden;

            ListadoDeTurnos.IsEnabled = false;
            ListadoDeTurnos.Visibility = Visibility.Hidden;

        }

        private void DirMedic_Checked(object sender, RoutedEventArgs e)
        {
            dPer.IsEnabled = true;
            dPer.Visibility = Visibility.Visible;

            desde.IsEnabled = true;
            desde.Visibility = Visibility.Visible;

            sta.IsEnabled = true;
            sta.Visibility = Visibility.Visible;

            hasta.IsEnabled = true;
            hasta.Visibility = Visibility.Visible;

            ListadoDePagos.IsEnabled = true;
            ListadoDePagos.Visibility = Visibility.Visible;

            ListadoDePacientesObra.IsEnabled = true;
            ListadoDePacientesObra.Visibility = Visibility.Visible;

            ListadoDePacientesMedico.IsEnabled = true;
            ListadoDePacientesMedico.Visibility = Visibility.Visible;

            ListadoDeTurnos.IsEnabled = true;
            ListadoDeTurnos.Visibility = Visibility.Visible;

            Medics.IsEnabled = false;
            Medics.Visibility = Visibility.Hidden;

            Pagos.IsEnabled = false;
            Pagos.Visibility = Visibility.Hidden;

            PacientesPorObra.IsEnabled = false;
            PacientesPorObra.Visibility = Visibility.Hidden;

            ProximosTurnos.IsEnabled = false;
            ProximosTurnos.Visibility = Visibility.Hidden;

            PacientesEnEspera.IsEnabled = false;
            PacientesEnEspera.Visibility = Visibility.Hidden;

            PacientesAtendidos.IsEnabled = false;
            PacientesAtendidos.Visibility = Visibility.Hidden;
        }

        private void cargarCrearMedico() {

            limpiarPantalla();

            labEsp.IsEnabled = true;
            labEsp.Visibility = Visibility.Visible;

            datEsp.IsEnabled = true;
            datEsp.Visibility = Visibility.Visible;

            butAgrEsp.IsEnabled = true;
            butAgrEsp.Visibility = Visibility.Visible;

            butQuitEsp.IsEnabled = true;
            butQuitEsp.Visibility = Visibility.Visible;

            combEsp.IsEnabled = true;
            combEsp.Visibility = Visibility.Visible;

            butGuardar.IsEnabled = true;
            butGuardar.Visibility = Visibility.Visible;

            butLimpiar.IsEnabled = true;
            butLimpiar.Visibility = Visibility.Visible;

            labNom.IsEnabled = true;
            labNom.Visibility = Visibility.Visible;

            labAp.IsEnabled = true;
            labAp.Visibility = Visibility.Visible;

            labDni.IsEnabled = true;
            labDni.Visibility = Visibility.Visible;

            labNat.IsEnabled = true;
            labNat.Visibility = Visibility.Visible;

            nameBox.IsEnabled = true;
            nameBox.Visibility = Visibility.Visible;

            apBox.IsEnabled = true;
            apBox.Visibility = Visibility.Visible;

            dniBox.IsEnabled = true;
            dniBox.Visibility = Visibility.Visible;

            natBox.IsEnabled = true;
            natBox.Visibility = Visibility.Visible;

            labDir.IsEnabled = true;
            labDir.Visibility = Visibility.Visible;

            labTel.IsEnabled = true;
            labTel.Visibility = Visibility.Visible;

            labCel.IsEnabled = true;
            labCel.Visibility = Visibility.Visible;

            labEm.IsEnabled = true;
            labEm.Visibility = Visibility.Visible;

            dirBox.IsEnabled = true;
            dirBox.Visibility = Visibility.Visible;

            telBox.IsEnabled = true;
            telBox.Visibility = Visibility.Visible;

            celBox.IsEnabled = true;
            celBox.Visibility = Visibility.Visible;

            emBox.IsEnabled = true;
            emBox.Visibility = Visibility.Visible;

            labFormas.IsEnabled = true;
            labFormas.Visibility = Visibility.Visible;

            checkEfec.IsEnabled = true;
            checkEfec.Visibility = Visibility.Visible;

            checkDep.IsEnabled = true;
            checkDep.Visibility = Visibility.Visible;

            checkObra.IsEnabled = true;
            checkObra.Visibility = Visibility.Visible;

            datObras.IsEnabled = true;
            datObras.Visibility = Visibility.Visible;

            combObras.IsEnabled = true;
            combObras.Visibility = Visibility.Visible;

            butAgr.IsEnabled = true;
            butAgr.Visibility = Visibility.Visible;

            butQuit.IsEnabled = true;
            butQuit.Visibility = Visibility.Visible;

            labHorarios.IsEnabled = true;
            labHorarios.Visibility = Visibility.Visible;

            checkLunM.IsEnabled = true;
            checkLunM.Visibility = Visibility.Visible;

            checkMarM.IsEnabled = true;
            checkMarM.Visibility = Visibility.Visible;

            checkMierM.IsEnabled = true;
            checkMierM.Visibility = Visibility.Visible;

            checkJueM.IsEnabled = true;
            checkJueM.Visibility = Visibility.Visible;

            checkVierM.IsEnabled = true;
            checkVierM.Visibility = Visibility.Visible;

            checkLunT.IsEnabled = true;
            checkLunT.Visibility = Visibility.Visible;

            checkMarT.IsEnabled = true;
            checkMarT.Visibility = Visibility.Visible;

            checkMierT.IsEnabled = true;
            checkMierT.Visibility = Visibility.Visible;

            checkJueT.IsEnabled = true;
            checkJueT.Visibility = Visibility.Visible;

            checkVierT.IsEnabled = true;
            checkVierT.Visibility = Visibility.Visible;

            labMan.IsEnabled = true;
            labMan.Visibility = Visibility.Visible;

            labTar.IsEnabled = true;
            labTar.Visibility = Visibility.Visible;

        }

        private void crear_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();

            


        }

        private void cargarCrearPaciente()
        {
            limpiarPantalla();

            butGuardar.IsEnabled = true;
            butGuardar.Visibility = Visibility.Visible;

            butLimpiar.IsEnabled = true;
            butLimpiar.Visibility = Visibility.Visible;

            labNom.IsEnabled = true;
            labNom.Visibility = Visibility.Visible;

            labAp.IsEnabled = true;
            labAp.Visibility = Visibility.Visible;

            labDni.IsEnabled = true;
            labDni.Visibility = Visibility.Visible;

            labNat.IsEnabled = true;
            labNat.Visibility = Visibility.Visible;

            nameBox.IsEnabled = true;
            nameBox.Visibility = Visibility.Visible;

            apBox.IsEnabled = true;
            apBox.Visibility = Visibility.Visible;

            dniBox.IsEnabled = true;
            dniBox.Visibility = Visibility.Visible;

            natBox.IsEnabled = true;
            natBox.Visibility = Visibility.Visible;

            labDir.IsEnabled = true;
            labDir.Visibility = Visibility.Visible;

            labTel.IsEnabled = true;
            labTel.Visibility = Visibility.Visible;

            labCel.IsEnabled = true;
            labCel.Visibility = Visibility.Visible;

            labEm.IsEnabled = true;
            labEm.Visibility = Visibility.Visible;

            dirBox.IsEnabled = true;
            dirBox.Visibility = Visibility.Visible;

            telBox.IsEnabled = true;
            telBox.Visibility = Visibility.Visible;

            celBox.IsEnabled = true;
            celBox.Visibility = Visibility.Visible;

            emBox.IsEnabled = true;
            emBox.Visibility = Visibility.Visible;
            
            checkObra.IsEnabled = true;
            checkObra.Visibility = Visibility.Visible;

            datObras.IsEnabled = true;
            datObras.Visibility = Visibility.Visible;

            combObras.IsEnabled = true;
            combObras.Visibility = Visibility.Visible;

            butAgr.IsEnabled = true;
            butAgr.Visibility = Visibility.Visible;

            butQuit.IsEnabled = true;
            butQuit.Visibility = Visibility.Visible;

            
        }
        
        private void medicos_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();
            mostrando = 1;
            lista.IsEnabled = true;
            lista.Visibility = Visibility.Visible;
            butNuevo.IsEnabled = true;
            butNuevo.Visibility = Visibility.Visible;
            butBuscar.IsEnabled = true;
            butBuscar.Visibility = Visibility.Visible;
            butEdit.IsEnabled = true;
            butEdit.Visibility = Visibility.Visible;
            butElim.IsEnabled = true;
            butElim.Visibility = Visibility.Visible;
        }

        private void pacientes_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();
            mostrando = 2;

            lista.IsEnabled = true;
            lista.Visibility = Visibility.Visible;
            butNuevo.IsEnabled = true;
            butNuevo.Visibility = Visibility.Visible;
            butBuscar.IsEnabled = true;
            butBuscar.Visibility = Visibility.Visible;
            butEdit.IsEnabled = true;
            butEdit.Visibility = Visibility.Visible;
            butElim.IsEnabled = true;
            butElim.Visibility = Visibility.Visible;
        }

        private void turnos_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();
            mostrando = 3;

            lista.IsEnabled = true;
            lista.Visibility = Visibility.Visible;
            butNuevo.IsEnabled = true;
            butNuevo.Visibility = Visibility.Visible;
            butBuscar.IsEnabled = true;
            butBuscar.Visibility = Visibility.Visible;
            butElim.IsEnabled = true;
            butElim.Visibility = Visibility.Visible;



        }

        private void pagos_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();
            mostrando = 4;

            lista.IsEnabled = true;
            lista.Visibility = Visibility.Visible;
            
        }
        
        private void butNuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();
            switch (mostrando) {
                case 1:
                    cargarCrearMedico();
                    break;
                case 2:
                    cargarCrearPaciente();
                    break;
                case 3:
                    cargarCrearTurno();
                    break;
                case 4:
                    cargarCrearPago();
                    break;
                                       
            }

        }

        private void cargarCrearPago() {


        }


        private dynamic GetLista(string nombreControlador, int? id1 = null, int? id2 = null, 
                                 DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
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

        
    }
}


