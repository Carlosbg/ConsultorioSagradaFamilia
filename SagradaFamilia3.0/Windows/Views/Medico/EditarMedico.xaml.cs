using ConsultorioSagradaFamilia.Models;
using SagradaFamilia3._0.Models;
using SagradaFamilia3._0.Utilities;
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

namespace SagradaFamilia3._0.Windows.Views.Medico
{
    /// <summary>
    /// Lógica de interacción para EditarMedico.xaml
    /// </summary>
    public partial class EditarMedico : Page
    {
        public ConsultorioSagradaFamilia.Models.Medico Medico { get; set; }
        public List<ConsultorioSagradaFamilia.Models.Especialidad> Especialidades { get; set; }
        public List<ConsultorioSagradaFamilia.Models.ObraSocial> ObrasSociales { get; set; }
        public List<HorarioAtencion> HorariosAtencion { get; set; }
        public List<int> Minutos = new List<int> { 0, 20, 40 };
        public List<int> MinutosCero = new List<int> { 0 };

        public List<ConsultorioSagradaFamilia.Models.Especialidad> EspecialidadesOriginales { get; set; }
        public List<ConsultorioSagradaFamilia.Models.ObraSocial> ObrasSocialesOriginales { get; set; }

        public EditarMedico(ConsultorioSagradaFamilia.Models.Medico medico)
        {
            InitializeComponent();
            Medico = medico;

            Apellido.Text = medico.Apellido;
            CUIL.Text = medico.CUIL;
            DNI.Text = medico.DNI.ToString();
            Domicilio.Text = medico.Domicilio;
            FechaNacimiento.SelectedDate = medico.FechaNacimiento;
            Mail.Text = medico.Mail;
            Matricula.Text = medico.Matricula.ToString();
            Monto.Text = medico.Monto.ToString("n2");
            Nombre.Text = medico.Nombre;
            Telefono.Text = medico.Telefono.ToString();
            Habilitado.IsChecked = medico.Habilitado;

            Especialidades = new List<ConsultorioSagradaFamilia.Models.Especialidad>();
            ObrasSociales = new List<ConsultorioSagradaFamilia.Models.ObraSocial>();
            HorariosAtencion = new List<HorarioAtencion>();

            EspecialidadesGrid.ItemsSource = Especialidades;
            ObrasSocialesGrid.ItemsSource = ObrasSociales;

            Dia Lunes = new Dia { Id = 1, Nombre = "Lunes" };
            Dia Martes = new Dia { Id = 2, Nombre = "Martes" };
            Dia Miércoles = new Dia { Id = 3, Nombre = "Miércoles" };
            Dia Jueves = new Dia { Id = 4, Nombre = "Jueves" };
            Dia Viernes = new Dia { Id = 5, Nombre = "Viernes" };
            Dia Sábado = new Dia { Id = 6, Nombre = "Sábado" };
            Dia Domingo = new Dia { Id = 7, Nombre = "Domingo" };

            List<Dia> Dias = new List<Dia> { Lunes, Martes, Miércoles, Jueves, Viernes, Sábado, Domingo };

            DiasCombobox.ItemsSource = Dias;
            DiasCombobox.DisplayMemberPath = "Nombre";
            DiasCombobox.SelectedValuePath = "Id";

            List<int> Horas = new List<int> { 8, 9, 10, 11, 12, 16, 17, 18, 19, 20 };

            HorasInicioCombobox.ItemsSource = Horas;
            MinutosInicioCombobox.ItemsSource = Minutos;

            HorasFinalCombobox.ItemsSource = Horas;
            MinutosFinalCombobox.ItemsSource = Minutos;

            HorariosGrid.ItemsSource = HorariosAtencion;

            Especialidades = DbContextSingleton.dbContext.GetEspecialidadesPorMedico(Medico.IdMedico);
            ObrasSociales = DbContextSingleton.dbContext.GetObraSocialesPorMedico(Medico.IdMedico);
            HorariosAtencion= DbContextSingleton.dbContext.GetHorariosAtencionPorMedico(Medico.IdMedico);

            EspecialidadesOriginales = DbContextSingleton.dbContext.GetEspecialidadesPorMedico(Medico.IdMedico);
            ObrasSocialesOriginales = DbContextSingleton.dbContext.GetObraSocialesPorMedico(Medico.IdMedico);

            EspecialidadesGrid.ItemsSource = Especialidades;
            ObrasSocialesGrid.ItemsSource = ObrasSociales;
            HorariosGrid.ItemsSource = HorariosAtencion;
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            Medicos medicos = new Medicos();
            Layout.Frame.Navigate(medicos);
        }

        private void ButtonEditar_Click(object sender, RoutedEventArgs e)
        {
            ConsultorioSagradaFamilia.Models.Medico medico = new ConsultorioSagradaFamilia.Models.Medico
            {
                IdMedico = Medico.IdMedico,
                Apellido = Apellido.Text,
                CUIL = CUIL.Text,
                DNI = int.Parse(DNI.Text),
                Domicilio = Domicilio.Text,
                FechaNacimiento = FechaNacimiento.SelectedDate.Value.Date,
                Mail = Mail.Text,
                Matricula = int.Parse(Matricula.Text),
                Monto = decimal.Parse(Monto.Text),
                Nombre = Nombre.Text,
                Telefono = int.Parse(Telefono.Text),
                Habilitado = Habilitado.IsChecked.GetValueOrDefault()
            };

            StatusMessage statusMessage = DbContextSingleton.dbContext.EditarMedico(medico);

            MessageBox.Show(statusMessage.Mensaje);

            if (statusMessage.Status == 0)
            {
                foreach (var item in EspecialidadesGrid.Items)
                {
                    if (EspecialidadesOriginales.Where(es => es.IdEspecialidad ==
                       ((ConsultorioSagradaFamilia.Models.Especialidad)item).IdEspecialidad).Count() == 0)
                    {
                        ConsultorioSagradaFamilia.Models.Especialidad especialidad = (ConsultorioSagradaFamilia.Models.Especialidad)item;

                        MedicoEspecialidad medicoEspecialidad = new MedicoEspecialidad();
                        medicoEspecialidad.IdMedico = Medico.IdMedico;
                        medicoEspecialidad.IdEspecialidad = especialidad.IdEspecialidad;

                        DbContextSingleton.dbContext.GuardarMedicoEspecialidad(medicoEspecialidad);
                    }
                }

                foreach (var item in EspecialidadesOriginales)
                {
                    if (Especialidades.Where(es => es.IdEspecialidad ==
                        item.IdEspecialidad).Count() == 0)
                    {
                        ConsultorioSagradaFamilia.Models.Especialidad especialidad = item;

                        MedicoEspecialidad medicoEspecialidad = new MedicoEspecialidad();
                        medicoEspecialidad.IdMedico = Medico.IdMedico;
                        medicoEspecialidad.IdEspecialidad = especialidad.IdEspecialidad;

                        DbContextSingleton.dbContext.BorrarMedicoEspecialidad(medicoEspecialidad);
                    }
                }

                foreach (var item in ObrasSocialesGrid.Items)
                {
                    if (ObrasSocialesOriginales.Where(es => es.IdObraSocial ==
                       ((ConsultorioSagradaFamilia.Models.ObraSocial)item).IdObraSocial).Count() == 0)
                    {
                        ConsultorioSagradaFamilia.Models.ObraSocial obraSocial = (ConsultorioSagradaFamilia.Models.ObraSocial)item;

                        ObraSocialMedico medicoObraSocial = new ObraSocialMedico();
                        medicoObraSocial.IdMedico = Medico.IdMedico;
                        medicoObraSocial.IdObraSocial = obraSocial.IdObraSocial;

                        DbContextSingleton.dbContext.GuardarObraSocialMedico(medicoObraSocial);
                    }
                }

                foreach (var item in ObrasSocialesOriginales)
                {
                    if (ObrasSociales.Where(es => es.IdObraSocial ==
                        item.IdObraSocial).Count() == 0)
                    {
                        ConsultorioSagradaFamilia.Models.ObraSocial obraSocial = item;

                        ObraSocialMedico medicoObraSocial = new ObraSocialMedico();
                        medicoObraSocial.IdMedico = Medico.IdMedico;
                        medicoObraSocial.IdObraSocial = obraSocial.IdObraSocial;

                        DbContextSingleton.dbContext.BorrarObraSocialMedico(medicoObraSocial);
                    }
                }

                foreach (var item in HorariosGrid.Items)
                {
                    HorarioAtencion horarioAtencion = (HorarioAtencion)item;
                    horarioAtencion.IdMedico = Medico.IdMedico;
                    if(horarioAtencion.IdHorarioAtencion == 0)
                    {
                        horarioAtencion.Habilitado = true;
                        DbContextSingleton.dbContext.GuardarHorarioAtencion((HorarioAtencion)item);
                    }
                    else
                    {
                        DbContextSingleton.dbContext.EditarHorarioAtencion((HorarioAtencion)item);
                    }                    
                }

                Medicos medicos = new Medicos();
                Layout.Frame.Navigate(medicos);
            }
        }

        private void TextBox_OnPreviewDNI(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);

            double d;
            e.Handled = !double.TryParse(text, out d);
        }

        private void TextBox_OnPreviewMatricula(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);

            double d;
            e.Handled = !double.TryParse(text, out d);
        }

        private void TextBox_OnPreviewCUIL(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);

            double d;
            e.Handled = !double.TryParse(text, out d);
        }

        private void TextBox_OnPreviewTelefono(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);

            double d;
            e.Handled = !double.TryParse(text, out d);
        }

        private void Nombre_TextChanged(object sender, EventArgs e)
        {
            string textoValidado = Validations.ValidarSoloTexto(Nombre.Text);
            Nombre.Text = textoValidado;
        }

        private void Apellido_TextChanged(object sender, EventArgs e)
        {
            string textoValidado = Validations.ValidarSoloTexto(Apellido.Text);
            Apellido.Text = textoValidado;
        }

        private void EspecialidadesCombobox_Initialized(object sender, EventArgs e)
        {
            EspecialidadesCombobox.ItemsSource = DbContextSingleton.dbContext.GetEspecialidades();

            EspecialidadesCombobox.DisplayMemberPath = "Nombre";
            EspecialidadesCombobox.SelectedValuePath = "IdEspecialidad";
        }

        private void ObrasSocialesCombobox_Initialized(object sender, EventArgs e)
        {
            ObrasSocialesCombobox.ItemsSource = DbContextSingleton.dbContext.GetObrasSociales();

            ObrasSocialesCombobox.DisplayMemberPath = "Nombre";
            ObrasSocialesCombobox.SelectedValuePath = "IdObraSocial";
        }

        private void AgregarEspecialidad_Click(object sender, RoutedEventArgs e)
        {
            var especialidad = (ConsultorioSagradaFamilia.Models.Especialidad)EspecialidadesCombobox.SelectedItem;

            if (especialidad != null)
            {
                foreach (var item in EspecialidadesGrid.Items)
                {
                    if (((ConsultorioSagradaFamilia.Models.Especialidad)item).IdEspecialidad == especialidad.IdEspecialidad)
                    {
                        MessageBox.Show("Ya se seleccionó esta especialidad");
                        return;
                    }
                }

                Especialidades.Add(especialidad);

                EspecialidadesGrid.ItemsSource = null;
                EspecialidadesGrid.ItemsSource = Especialidades;
            }
        }

        private void AgregarObraSocial_Click(object sender, RoutedEventArgs e)
        {
            var obraSocial = (ConsultorioSagradaFamilia.Models.ObraSocial)ObrasSocialesCombobox.SelectedItem;

            if (obraSocial != null)
            {
                foreach (var item in ObrasSocialesGrid.Items)
                {
                    if (((ConsultorioSagradaFamilia.Models.ObraSocial)item).IdObraSocial == obraSocial.IdObraSocial)
                    {
                        MessageBox.Show("Ya se seleccionó esta obra social");
                        return;
                    }
                }

                ObrasSociales.Add(obraSocial);

                ObrasSocialesGrid.ItemsSource = null;
                ObrasSocialesGrid.ItemsSource = ObrasSociales;
            }
        }

        private void BorrarEspecialidad_Click(object sender, RoutedEventArgs e)
        {
            ConsultorioSagradaFamilia.Models.Especialidad seleccion = (ConsultorioSagradaFamilia.Models.Especialidad)EspecialidadesGrid.SelectedItem;

            if (seleccion == null)
            {
                MessageBox.Show("Debe seleccionar una especialidad");
                return;
            }

            foreach (var item in Especialidades)
            {
                if (item.IdEspecialidad == seleccion.IdEspecialidad)
                {
                    Especialidades.Remove(item);

                    EspecialidadesGrid.ItemsSource = null;
                    EspecialidadesGrid.ItemsSource = Especialidades;

                    break;
                }
            }
        }

        private void BorrarObraSocial_Click(object sender, RoutedEventArgs e)
        {
            ConsultorioSagradaFamilia.Models.ObraSocial seleccion = (ConsultorioSagradaFamilia.Models.ObraSocial)ObrasSocialesGrid.SelectedItem;

            if (seleccion == null)
            {
                MessageBox.Show("Debe seleccionar una obra social");
                return;
            }

            foreach (var item in ObrasSociales)
            {
                if (item.IdObraSocial == seleccion.IdObraSocial)
                {
                    ObrasSociales.Remove(item);

                    ObrasSocialesGrid.ItemsSource = null;
                    ObrasSocialesGrid.ItemsSource = ObrasSociales;

                    break;
                }
            }
        }

        private void AgregarHorario_Click(object sender, RoutedEventArgs e)
        {
            if (DiasCombobox.SelectedValue == null) { MessageBox.Show("Debe indicar un dia"); return; };
            if (HorasInicioCombobox.SelectedValue == null) { MessageBox.Show("Debe indicar las horas del horario inicial"); return; };
            if (MinutosInicioCombobox.SelectedValue == null) { MessageBox.Show("Debe indicar los minutos del horario inicial"); return; };
            if (HorasFinalCombobox.SelectedValue == null) { MessageBox.Show("Debe indicar las horas del horario final"); return; };
            if (MinutosFinalCombobox.SelectedValue == null) { MessageBox.Show("Debe indicar los minutos del horario final"); return; };

            if ((int)HorasInicioCombobox.SelectedValue > (int)HorasFinalCombobox.SelectedValue)
            {
                MessageBox.Show("El horario de inicio no puede ser mayor al final");
                return;
            }
            else
            {
                if ((int)HorasInicioCombobox.SelectedValue == (int)HorasFinalCombobox.SelectedValue &&
                   (int)MinutosInicioCombobox.SelectedValue > (int)MinutosFinalCombobox.SelectedValue)
                {
                    MessageBox.Show("El horario de inicio no puede ser mayor al final");
                    return;
                }
            }

            HorarioAtencion horarioAtencion = new HorarioAtencion
            {
                Habilitado = true,
                HorarioFinal = new TimeSpan((int)HorasFinalCombobox.SelectedValue, (int)MinutosFinalCombobox.SelectedValue, 0),
                HorarioInicio = new TimeSpan((int)HorasInicioCombobox.SelectedValue, (int)MinutosInicioCombobox.SelectedValue, 0),
                IdDia = (int)DiasCombobox.SelectedValue
            };

            foreach (var item in HorariosAtencion)
            {
                if (item.IdDia == horarioAtencion.IdDia)
                {
                    if (horarioAtencion.HorarioInicio >= item.HorarioInicio && horarioAtencion.HorarioFinal <= item.HorarioFinal &&
                        item.Habilitado == true)
                    {
                        MessageBox.Show("Este horario coincide con uno existente");
                        return;
                    }
                }
            }

            HorariosAtencion.Add(horarioAtencion);

            HorariosGrid.ItemsSource = null;
            HorariosGrid.ItemsSource = HorariosAtencion.OrderByDescending(p => p.Habilitado).ToList();

            HorariosGrid.Columns[1].Visibility = Visibility.Collapsed;
            HorariosGrid.Columns[2].Visibility = Visibility.Collapsed;
            HorariosGrid.Columns[3].Visibility = Visibility.Collapsed;
            HorariosGrid.Columns[4].Header = "Horario Inicio";
            HorariosGrid.Columns[5].Header = "Horario Final";

        }

        private void BorrarHorario_Click(object sender, RoutedEventArgs e)
        {
            HorarioAtencion seleccion = (HorarioAtencion)HorariosGrid.SelectedItem;

            if (seleccion == null)
            {
                MessageBox.Show("Debe seleccionar un horario");
                return;
            }

            foreach (var item in HorariosAtencion)
            {
                if (item.IdHorarioAtencion == seleccion.IdHorarioAtencion)
                {
                    if(item.Habilitado == true)
                    {
                        item.Habilitado = false;

                        HorariosGrid.ItemsSource = null;
                        HorariosGrid.ItemsSource = HorariosAtencion.OrderByDescending(p => p.Habilitado).ToList();

                        HorariosGrid.Columns[1].Visibility = Visibility.Collapsed;
                        HorariosGrid.Columns[2].Visibility = Visibility.Collapsed;
                        HorariosGrid.Columns[3].Visibility = Visibility.Collapsed;
                        HorariosGrid.Columns[4].Header = "Horario Inicio";
                        HorariosGrid.Columns[5].Header = "Horario Final";
                    }                   

                    break;
                }
            }
        }

        private void HorariosGrid_Loaded(object sender, RoutedEventArgs e)
        {
            HorariosGrid.Columns[1].Visibility = Visibility.Collapsed;
            HorariosGrid.Columns[2].Visibility = Visibility.Collapsed;
            HorariosGrid.Columns[3].Visibility = Visibility.Collapsed;
            HorariosGrid.Columns[4].Header = "Horario Inicio";
            HorariosGrid.Columns[5].Header = "Horario Final";
        }

        private void HorasInicioCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HorasInicioCombobox.SelectedValue != null)
            {
                if ((int)HorasInicioCombobox.SelectedValue == 20)
                {
                    MinutosInicioCombobox.ItemsSource = MinutosCero;
                }
                else
                {
                    MinutosInicioCombobox.ItemsSource = Minutos;
                }
            }
        }

        private void HorasFinalCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HorasFinalCombobox.SelectedValue != null)
            {
                if ((int)HorasFinalCombobox.SelectedValue == 20)
                {
                    MinutosFinalCombobox.ItemsSource = MinutosCero;
                }
                else
                {
                    MinutosFinalCombobox.ItemsSource = Minutos;
                }
            }
        }
    }
}
