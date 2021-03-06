﻿using System;
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
using SAPBusinessObjects.WPF.Viewer;

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

            TurnoE.IsEnabled = true;
            TurnoE.Visibility = Visibility.Visible;

            Paciente.IsEnabled = true;
            Paciente.Visibility = Visibility.Visible;

            labPac.IsEnabled = true;
            labPac.Visibility = Visibility.Visible;

            butGuardar.IsEnabled = true;
            butGuardar.Visibility = Visibility.Visible;

            butLimpiar.IsEnabled = true;
            butLimpiar.Visibility = Visibility.Visible;

            esp.IsEnabled = true;
            esp.Visibility = Visibility.Visible;

            fech.IsEnabled = true;
            fech.Visibility = Visibility.Visible;

            hor.IsEnabled = true;
            hor.Visibility = Visibility.Visible;

            med.IsEnabled = true;
            med.Visibility = Visibility.Visible;

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

            butImprimir.Visibility = Visibility.Visible;

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

            Viewer.Visibility = Visibility.Hidden;

            labBusqNombre.IsEnabled = false;
            labBusqNombre.Visibility = Visibility.Hidden;

            labBusqApellido.IsEnabled = false;
            labBusqApellido.Visibility = Visibility.Hidden;

            busqNombreBox.IsEnabled = false;
            busqNombreBox.Visibility = Visibility.Hidden;

            busqApellidoBox.IsEnabled = false;
            busqApellidoBox.Visibility = Visibility.Hidden;

            combBuscMedico.IsEnabled = false;
            combBuscMedico.Visibility = Visibility.Hidden;

            combBuscPaciente.IsEnabled = false;
            combBuscPaciente.Visibility = Visibility.Hidden;

            dateBusqDesde.IsEnabled = false;
            dateBusqDesde.Visibility = Visibility.Hidden;

            labBusqPaciente.IsEnabled = false;
            labBusqPaciente.Visibility = Visibility.Hidden;

            labBusqMedico.IsEnabled = false;
            labBusqMedico.Visibility = Visibility.Hidden;

            labBusqDesde.IsEnabled = false;
            labBusqDesde.Visibility = Visibility.Hidden;

            dateBusqHasta.IsEnabled = false;
            dateBusqHasta.Visibility = Visibility.Hidden;

            labBusqHasta.IsEnabled = false;
            labBusqHasta.Visibility = Visibility.Hidden;




            CombEntidad.IsEnabled = false;
            CombEntidad.Visibility = Visibility.Hidden;

            listaTurnos.IsEnabled = false;
            listaTurnos.Visibility = Visibility.Hidden;

            CombForma.IsEnabled = false;
            CombForma.Visibility = Visibility.Hidden;

            labFdp.IsEnabled = false;
            labFdp.Visibility = Visibility.Hidden;

            labEnt.IsEnabled = false;
            labEnt.Visibility = Visibility.Hidden;

            labMed.IsEnabled = false;
            labMed.Visibility = Visibility.Hidden;

            labPaciente.IsEnabled = false;
            labPaciente.Visibility = Visibility.Hidden;

            labMont.IsEnabled = false;
            labMont.Visibility = Visibility.Hidden;

            combMed.IsEnabled = false;
            combMed.Visibility = Visibility.Hidden;

            combPac.IsEnabled = false;
            combPac.Visibility = Visibility.Hidden;

            montoBox.IsEnabled = false;
            montoBox.Visibility = Visibility.Hidden;

            listaTurnos.IsEnabled = false;
            listaTurnos.Visibility = Visibility.Hidden;

            TurnoE.IsEnabled = false;
            TurnoE.Visibility = Visibility.Hidden;

            Paciente.IsEnabled = false;
            Paciente.Visibility = Visibility.Hidden;

            labPac.IsEnabled = false;
            labPac.Visibility = Visibility.Hidden;

            butImprimir.IsEnabled = false;
            butImprimir.Visibility = Visibility.Hidden;

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


            esp.IsEnabled = false;
            esp.Visibility = Visibility.Hidden;

            fech.IsEnabled = false;
            fech.Visibility = Visibility.Hidden;

            hor.IsEnabled = false;
            hor.Visibility = Visibility.Hidden;

            med.IsEnabled = false;
            med.Visibility = Visibility.Hidden;            

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

            butPagar.IsEnabled = false;
            butPagar.Visibility = Visibility.Hidden;
        }

        private void generarInforme_Click(object sender, RoutedEventArgs e)
        {
            limpiarPantalla();
            cargarGenerarInforme();
            Viewer.Visibility = Visibility.Visible;
        }

        private void Medic_Checked(object sender, RoutedEventArgs e)
        {
            butImprimir.IsEnabled = true;

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
            butImprimir.IsEnabled = true;

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
            LimpiarCamposSiEstabaEditando();

            if (mostrando != 1) lista.ItemsSource = null;
            limpiarPantalla();
            mostrando = 1;

            labBusqNombre.IsEnabled = true;
            labBusqNombre.Visibility = Visibility.Visible;

            labBusqApellido.IsEnabled = true;
            labBusqApellido.Visibility = Visibility.Visible;

            busqNombreBox.IsEnabled = true;
            busqNombreBox.Visibility = Visibility.Visible;

            busqApellidoBox.IsEnabled = true;
            busqApellidoBox.Visibility = Visibility.Visible;

            listarNuevoEditarBuscarEliminar();

            horarioUsadoEnEditTurno = null;
            
        }

        private void pacientes_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposSiEstabaEditando();

            if (mostrando != 2) lista.ItemsSource = null;
            limpiarPantalla();
            mostrando = 2;

            labBusqNombre.IsEnabled = true;
            labBusqNombre.Visibility = Visibility.Visible;

            labBusqApellido.IsEnabled = true;
            labBusqApellido.Visibility = Visibility.Visible;

            busqNombreBox.IsEnabled = true;
            busqNombreBox.Visibility = Visibility.Visible;

            busqApellidoBox.IsEnabled = true;
            busqApellidoBox.Visibility = Visibility.Visible;

            listarNuevoEditarBuscarEliminar();

            horarioUsadoEnEditTurno = null;
        }

        private void turnos_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposSiEstabaEditando();

            if (mostrando != 3) lista.ItemsSource = null;
            limpiarPantalla();
            mostrando = 3;

            combBuscMedico.IsEnabled = true;
            combBuscMedico.Visibility = Visibility.Visible;

            combBuscPaciente.IsEnabled = true;
            combBuscPaciente.Visibility = Visibility.Visible;

            dateBusqDesde.IsEnabled = true;
            dateBusqDesde.Visibility = Visibility.Visible;

            labBusqPaciente.IsEnabled = true;
            labBusqPaciente.Visibility = Visibility.Visible;

            labBusqMedico.IsEnabled = true;
            labBusqMedico.Visibility = Visibility.Visible;

            labBusqDesde.IsEnabled = true;
            labBusqDesde.Visibility = Visibility.Visible;

            dateBusqHasta.IsEnabled = true;
            dateBusqHasta.Visibility = Visibility.Visible;

            labBusqHasta.IsEnabled = true;
            labBusqHasta.Visibility = Visibility.Visible;

            listarNuevoEditarBuscarEliminar();

            horarioUsadoEnEditTurno = null;
        }

        private void pagos_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposSiEstabaEditando();

            if (mostrando != 4) lista.ItemsSource = null;
            limpiarPantalla();
            mostrando = 4;

            combBuscMedico.IsEnabled = true;
            combBuscMedico.Visibility = Visibility.Visible;

            combBuscPaciente.IsEnabled = true;
            combBuscPaciente.Visibility = Visibility.Visible;

            dateBusqDesde.IsEnabled = true;
            dateBusqDesde.Visibility = Visibility.Visible;

            labBusqPaciente.IsEnabled = true;
            labBusqPaciente.Visibility = Visibility.Visible;

            labBusqMedico.IsEnabled = true;
            labBusqMedico.Visibility = Visibility.Visible;

            labBusqDesde.IsEnabled = true;
            labBusqDesde.Visibility = Visibility.Visible;

            dateBusqHasta.IsEnabled = true;
            dateBusqHasta.Visibility = Visibility.Visible;

            labBusqHasta.IsEnabled = true;
            labBusqHasta.Visibility = Visibility.Visible;

            listarNuevoEditarBuscarEliminar();

            horarioUsadoEnEditTurno = null;
        }

        private void listarNuevoEditarBuscarEliminar() {

            
            lista.IsEnabled = true;
            lista.Visibility = Visibility.Visible;
            butNuevo.IsEnabled = true;
            butNuevo.Visibility = Visibility.Visible;
            butEdit.IsEnabled = false;  //se habilita al seleccionar un elemento de la lista

            butEdit.Visibility = Visibility.Visible;
            butBuscar.IsEnabled = true;
            butBuscar.Visibility = Visibility.Visible;
            butElim.IsEnabled = false;  //se habilita al seleccionar un elemento de la lista


            butElim.Visibility = Visibility.Visible;

            if(mostrando == 3)
            {
                butPagar.IsEnabled = false;  //se habilita al seleccionar un elemento de la lista
                butPagar.Visibility = Visibility.Visible;
            }
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
            limpiarPantalla();

            butLimpiar.IsEnabled = true;
            butLimpiar.Visibility = Visibility.Visible;

            butGuardar.IsEnabled = true;
            butGuardar.Visibility = Visibility.Visible;

            CombEntidad.IsEnabled = true;
            CombEntidad.Visibility = Visibility.Visible;

            listaTurnos.IsEnabled = true;
            listaTurnos.Visibility = Visibility.Visible;

            CombForma.IsEnabled = true;
            CombForma.Visibility = Visibility.Visible;

            labFdp.IsEnabled = true;
            labFdp.Visibility = Visibility.Visible;

            labEnt.IsEnabled = true;
            labEnt.Visibility = Visibility.Visible;

            labMed.IsEnabled = true;
            labMed.Visibility = Visibility.Visible;

            labPaciente.IsEnabled = true;
            labPaciente.Visibility = Visibility.Visible;

            labMont.IsEnabled = true;
            labMont.Visibility = Visibility.Visible;

            combMed.IsEnabled = true;
            combMed.Visibility = Visibility.Visible;

            combPac.IsEnabled = true;
            combPac.Visibility = Visibility.Visible;

            montoBox.IsEnabled = true;
            montoBox.Visibility = Visibility.Visible;

            listaTurnos.IsEnabled = true;
            listaTurnos.Visibility = Visibility.Visible;



        }

        private void butImprimir_Click(object sender, RoutedEventArgs e)
        {
            List<dynamic> pagosPorFormaPagoDynamic = GetLista("PagosPorFormaPago");
            List<PagosPorFormaPago> pagos = new List<PagosPorFormaPago>();

            foreach (var pagoDynamic in pagosPorFormaPagoDynamic)
            {
                PagosPorFormaPago pago = new PagosPorFormaPago
                {
                    FormaPago = pagoDynamic.FormaPago,
                    IdFormaPago = pagoDynamic.IdFormaPago,
                    IdObraSocial = pagoDynamic.IdObraSocial,
                    Monto = pagoDynamic.Monto,
                    NombreObraSocial = pagoDynamic.NombreObraSocial,
                    Fecha = pagoDynamic.Fecha,
                    IdMedico = pagoDynamic.IdMedico,
                    IdPaciente = pagoDynamic.IdPaciente,
                    NombreMedico = pagoDynamic.NombreMedico,
                    NombrePaciente = pagoDynamic.NombrePaciente
                };

                pagos.Add(pago);
            }

            Reports.PagosPorFormaPago report = new Reports.PagosPorFormaPago();
            report.SetDataSource(pagos);

            //CrystalReportsViewer rptViewer = new CrystalReportViewer();
            // WindowsFormsHost host = new WindowsFormsHost();
            Viewer.ViewerCore.ReportSource = report;
            //host.Child = rptViewer;
            //reportGrid.Children.Add(host);
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

        private void butEdit_Click(object sender, RoutedEventArgs e)
        {
            switch (mostrando)
            {
                case 1:
                    editarPaciente();
                    break;
                case 2:
                    editarPaciente();
                    break;
                case 3:
                    TurnosPorPaciente seleccion = (TurnosPorPaciente)lista.SelectedItem;
                    if (seleccion.Atendido)
                    {
                        MessageBox.Show("Este turno no puede ser editado porque ya fue atendido.");
                        return;
                    }
                    editarTurno(seleccion.IdTurno);
                    break;
                case 4:
                    editarPaciente();
                    break;

            }
        }

        private void butEliminar_Click(object sender, RoutedEventArgs e)
        {
            switch (mostrando)
            {
                case 1:
                    //medico
                    break;
                case 2:
                    //paciente
                    break;
                case 3:
                    TurnosPorPaciente seleccion = (TurnosPorPaciente)lista.SelectedItem;
                    if (seleccion.Atendido)
                    {
                        MessageBox.Show("Este turno no puede ser eliminado porque ya fue atendido.");
                        return;
                    }

                    MessageBoxResult messageBoxResult = MessageBox.Show("¿Esta seguro que desea eliminar el turno" +
                        " del paciente " + seleccion.NombrePaciente + " con el medico " + seleccion.NombreMedico +
                        " del dia " + seleccion.FechaString + "?", 
                        "Confirmacion de borrado", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        EliminarTurno(seleccion.IdTurno);
                    }
                    break;
                case 4:
                    //pago
                    break;

            }
        }

        private void butDesconectar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            main.Focus();
            this.Close();
        }

        private void editarPaciente()
        {
            cargarCrearPaciente();
            //llenar con datos de la bd

        }

        private void editarTurno(int idTurno)
        {
            cargarCrearTurno();

            CargarTurnoParaEditar(idTurno);

            editando = true;
        }

        private void editarPago()
        {
            cargarCrearPago();
            //llenar con datos de la bd

        }

        private void lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            butEdit.IsEnabled = true;
            butElim.IsEnabled = true;
            
            if(mostrando == 3)
            {
                butPagar.IsEnabled = true;
            }
        }

        private void butLimpiar_Click(object sender, RoutedEventArgs e)
        {
            switch (mostrando) {

                case 1:
                    limpiarMedico();
                    break;
                case 2:
                    limpiarPaciente();
                    break;
                case 3:
                    limpiarTurno();
                    break;
                case 4:
                    limpiarPago();
                    break;
            }
        }

        private void limpiarMedico()
        {
            nameBox.Clear();
            apBox.Clear();
            dniBox.Clear();
            natBox.Clear();
            dirBox.Clear();
            telBox.Clear();
            celBox.Clear();
            emBox.Clear();
            datObras.Items.Clear();
            datEsp.Items.Clear();
            combObras.SelectedItem = null;
            combEsp.SelectedItem = null;
            checkObra.IsChecked = false;

            checkEfec.IsChecked = false;
            checkDep.IsChecked = false;
            checkObra.IsChecked = false;

            checkLunM.IsChecked = false;
            checkMarM.IsChecked = false;
            checkMierM.IsChecked = false;
            checkJueM.IsChecked = false;
            checkVierM.IsChecked = false;

            checkLunT.IsChecked = false;
            checkMarT.IsChecked = false;
            checkMierT.IsChecked = false;
            checkJueT.IsChecked = false;
            checkVierT.IsChecked = false;
        }


        private void limpiarPaciente()
        {
            nameBox.Clear();
            apBox.Clear();
            dniBox.Clear();
            natBox.Clear();
            dirBox.Clear();
            telBox.Clear();
            celBox.Clear();
            emBox.Clear();
            datObras.Items.Clear();
            combObras.SelectedItem = null;
            checkObra.IsChecked = false;


        }
        private void limpiarTurno()
        {
            Especialidad.SelectedItem = null;
            Fecha.SelectedItem = null;
            Hora.SelectedItem = null;
            Medico.SelectedItem = null;
            Paciente.SelectedItem = null;
            TurnoM.IsChecked = false;
            TurnoE.IsChecked = false;
            TurnoT.IsChecked = false;


        }
        private void limpiarPago()
        {

        }
    }
}


