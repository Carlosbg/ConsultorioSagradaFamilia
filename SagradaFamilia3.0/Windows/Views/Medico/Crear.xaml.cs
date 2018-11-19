using ConsultorioSagradaFamilia.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using SagradaFamilia3._0.Models;
using SagradaFamilia3._0.Utilities;
using SagradaFamilia3._0.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SagradaFamilia3._0.Windows.Views.Medico
{
    public class Dia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    /// <summary>
    /// Lógica de interacción para Crear.xaml
    /// </summary>
    public partial class Crear : Page
    {
        public List<ConsultorioSagradaFamilia.Models.Especialidad> Especialidades { get; set; }
        public List<ConsultorioSagradaFamilia.Models.ObraSocial> ObrasSociales { get; set; }
        public List<HorarioAtencion> HorariosAtencion { get; set; }
        public List<int> Minutos = new List<int> { 0, 20, 40 };
        public List<int> MinutosCero = new List<int> { 0 };

        public Crear()
        {
            InitializeComponent();

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
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            Medicos medicos = new Medicos();
            Layout.Frame.Navigate(medicos);
        }

        private void ButtonCrear_Click(object sender, RoutedEventArgs e)
        {
            if (Apellido.Text == "")
            {
                MessageBox.Show("Debe indicar el Apellido");
                return;
            }
            if (CUIL.Text == "")
            {
                MessageBox.Show("Debe indicar el CUIL");
                return;
            }
            if (DNI.Text == "")
            {
                MessageBox.Show("Debe indicar el DNI");
                return;
            }
            if (Domicilio.Text == "")
            {
                MessageBox.Show("Debe indicar el Domicilio");
                return;
            }
            if (FechaNacimiento.SelectedDate == null)
            {
                MessageBox.Show("Debe indicar la Fecha de Nacimiento");
                return;
            }
            if (Mail.Text == "")
            {
                MessageBox.Show("Debe indicar el Email");
                return;
            }
            if (Matricula.Text == "")
            {
                MessageBox.Show("Debe indicar la Matrícula");
                return;
            }
            if (Monto.Text == "")
            {
                MessageBox.Show("Debe indicar el Monto");
                return;
            }
            if (Nombre.Text == "")
            {
                MessageBox.Show("Debe indicar el Nombre");
                return;
            }
            if (Telefono.Text == "")
            {
                MessageBox.Show("Debe indicar el Teléfono");
                return;
            }

            if (Contraseña.Password != Confirmacion.Password)
            {
                MessageBox.Show("La confirmación y la contraseña deben ser iguales");
                return;
            }

            ConsultorioSagradaFamilia.Models.Usuario usuario = new ConsultorioSagradaFamilia.Models.Usuario
            {
                Email = Mail.Text,
                Password = Contraseña.Password
            };

            var client = new RestClient("http://consultoriosagradafamilia.azurewebsites.net/api/Account/Register");
            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "ff0920db-2463-74a8-8bf4-0bf3969e4083");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");

            string body = "Email=" + usuario.Email +
                            "&Password=" + usuario.Password +
                            "&ConfirmPassword=" + Confirmacion.Password +
                            "&Roles[0]=Médico";

            request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            var content = response.Content;

            if (content != "")
            {
                dynamic stuff = JObject.Parse(content);

                if (stuff.Message.ToString() == "La solicitud no es válida.")
                {
                    foreach (var error in stuff.ModelState)
                    {
                        MessageBox.Show(error.ToString());
                    }
                    return;
                }
                else if (stuff.error == null)
                {
                    //MessageBox.Show("Usuario creado.");
                }
                else
                {
                    MessageBox.Show("Hubo un problema al registrar el usuario");
                    return;
                }
            }
            else
            {
                //MessageBox.Show("Usuario creado");
            }

            ConsultorioSagradaFamilia.Models.Medico medico = new ConsultorioSagradaFamilia.Models.Medico
            {
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
                Habilitado = true
            };

            StatusMessage statusMessage = DbContextSingleton.dbContext.GuardarMedico(medico);

            MessageBox.Show(statusMessage.Mensaje);

            if (statusMessage.Status == 0)
            {
                ConsultorioSagradaFamilia.Models.Medico medicoConId = DbContextSingleton.dbContext.GetLastMedico();

                foreach (var item in EspecialidadesGrid.Items)
                {
                    ConsultorioSagradaFamilia.Models.Especialidad especialidad = (ConsultorioSagradaFamilia.Models.Especialidad)item;

                    MedicoEspecialidad medicoEspecialidad = new MedicoEspecialidad();
                    medicoEspecialidad.IdMedico = medicoConId.IdMedico;
                    medicoEspecialidad.IdEspecialidad = especialidad.IdEspecialidad;

                    DbContextSingleton.dbContext.GuardarMedicoEspecialidad(medicoEspecialidad);             
                }

                foreach (var item in ObrasSocialesGrid.Items)
                {
                    ConsultorioSagradaFamilia.Models.ObraSocial obraSocial = (ConsultorioSagradaFamilia.Models.ObraSocial)item;

                    ObraSocialMedico obraSocialMedico = new ObraSocialMedico();
                    obraSocialMedico.IdMedico = medicoConId.IdMedico;
                    obraSocialMedico.IdObraSocial = obraSocial.IdObraSocial;

                    DbContextSingleton.dbContext.GuardarObraSocialMedico(obraSocialMedico);
                }

                foreach(var item in HorariosGrid.Items)
                {
                    HorarioAtencion horarioAtencion = (HorarioAtencion)item;
                    horarioAtencion.IdMedico = medicoConId.IdMedico;

                    DbContextSingleton.dbContext.GuardarHorarioAtencion((HorarioAtencion)item);
                }

                Medicos medicos2 = new Medicos();
                Layout.Frame.Navigate(medicos2);
            }
        }

        //private void EspecialidadesCombobox_Initialized()

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

            if(especialidad != null)
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

            if(obraSocial != null)
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

            if(seleccion == null)
            {
                MessageBox.Show("Debe seleccionar una especialidad");
                return;
            }

            foreach (var item in Especialidades)
            {
                if(item.IdEspecialidad == seleccion.IdEspecialidad)
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
                if (item.IdObraSocial== seleccion.IdObraSocial)
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

            if((int)HorasInicioCombobox.SelectedValue > (int)HorasFinalCombobox.SelectedValue)
            {
                MessageBox.Show("El horario de inicio no puede ser mayor al final");
                return;
            }
            else
            {
                if((int)HorasInicioCombobox.SelectedValue == (int)HorasFinalCombobox.SelectedValue &&
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
                if(item.IdDia == horarioAtencion.IdDia)
                {
                    if(horarioAtencion.HorarioInicio >= item.HorarioInicio && horarioAtencion.HorarioFinal <= item.HorarioFinal)
                    {
                        MessageBox.Show("Este horario coincide con uno existente");
                        return;
                    }
                }
            }

            HorariosAtencion.Add(horarioAtencion);

            HorariosGrid.ItemsSource = null;
            HorariosGrid.ItemsSource = HorariosAtencion;

            HorariosGrid.Columns[1].Visibility = Visibility.Collapsed;
            HorariosGrid.Columns[2].Visibility = Visibility.Collapsed;
            HorariosGrid.Columns[3].Visibility = Visibility.Collapsed;
            HorariosGrid.Columns[4].Header = "Horario Inicio";
            HorariosGrid.Columns[5].Header = "Horario Final";

        }

        private void BorrarHorario_Click(object sender, RoutedEventArgs e)
        {
            HorarioAtencion seleccion = (HorarioAtencion)HorariosGrid.SelectedItem;

            if(seleccion == null)
            {
                MessageBox.Show("Debe seleccionar un horario");
                return;
            }

            foreach (var item in HorariosAtencion)
            {
                if (item.IdHorarioAtencion == seleccion.IdHorarioAtencion)
                {
                    HorariosAtencion.Remove(item);

                    HorariosGrid.ItemsSource = null;
                    HorariosGrid.ItemsSource = HorariosAtencion;

                    HorariosGrid.Columns[1].Visibility = Visibility.Collapsed;
                    HorariosGrid.Columns[2].Visibility = Visibility.Collapsed;
                    HorariosGrid.Columns[3].Visibility = Visibility.Collapsed;
                    HorariosGrid.Columns[4].Header = "Horario Inicio";
                    HorariosGrid.Columns[5].Header = "Horario Final";

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
            if(HorasInicioCombobox.SelectedValue != null)
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
            if(HorasFinalCombobox.SelectedValue != null)
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

namespace Behaviors
{
    public class TextBoxInputBehavior : Behavior<TextBox>
    {
        const NumberStyles validNumberStyles = NumberStyles.AllowDecimalPoint |
                                                   NumberStyles.AllowThousands |
                                                   NumberStyles.AllowLeadingSign;
        public TextBoxInputBehavior()
        {
            this.InputMode = TextBoxInputMode.None;
            this.JustPositivDecimalInput = false;
        }

        public TextBoxInputMode InputMode { get; set; }


        public static readonly DependencyProperty JustPositivDecimalInputProperty =
         DependencyProperty.Register("JustPositivDecimalInput", typeof(bool),
         typeof(TextBoxInputBehavior), new FrameworkPropertyMetadata(false));

        public bool JustPositivDecimalInput
        {
            get { return (bool)GetValue(JustPositivDecimalInputProperty); }
            set { SetValue(JustPositivDecimalInputProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown += AssociatedObjectPreviewKeyDown;

            DataObject.AddPastingHandler(AssociatedObject, Pasting);

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown -= AssociatedObjectPreviewKeyDown;

            DataObject.RemovePastingHandler(AssociatedObject, Pasting);
        }

        private void Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var pastedText = (string)e.DataObject.GetData(typeof(string));

                if (!this.IsValidInput(this.GetText(pastedText)))
                {
                    System.Media.SystemSounds.Beep.Play();
                    e.CancelCommand();
                }
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
                e.CancelCommand();
            }
        }

        private void AssociatedObjectPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (!this.IsValidInput(this.GetText(" ")))
                {
                    System.Media.SystemSounds.Beep.Play();
                    e.Handled = true;
                }
            }
        }

        private void AssociatedObjectPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!this.IsValidInput(this.GetText(e.Text)))
            {
                System.Media.SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private string GetText(string input)
        {
            var txt = this.AssociatedObject;

            int selectionStart = txt.SelectionStart;
            if (txt.Text.Length < selectionStart)
                selectionStart = txt.Text.Length;

            int selectionLength = txt.SelectionLength;
            if (txt.Text.Length < selectionStart + selectionLength)
                selectionLength = txt.Text.Length - selectionStart;

            var realtext = txt.Text.Remove(selectionStart, selectionLength);

            int caretIndex = txt.CaretIndex;
            if (realtext.Length < caretIndex)
                caretIndex = realtext.Length;

            var newtext = realtext.Insert(caretIndex, input);

            return newtext;
        }

        private bool IsValidInput(string input)
        {
            switch (InputMode)
            {
                case TextBoxInputMode.None:
                    return true;
                case TextBoxInputMode.DigitInput:
                    return CheckIsDigit(input);

                case TextBoxInputMode.DecimalInput:
                    decimal d;
                    //wen mehr als ein Komma
                    if (input.ToCharArray().Where(x => x == ',').Count() > 1)
                        return false;


                    if (input.Contains("-"))
                    {
                        if (this.JustPositivDecimalInput)
                            return false;


                        if (input.IndexOf("-", StringComparison.Ordinal) > 0)
                            return false;

                        if (input.ToCharArray().Count(x => x == '-') > 1)
                            return false;

                        //minus einmal am anfang zulässig
                        if (input.Length == 1)
                            return true;
                    }

                    var result = decimal.TryParse(input, validNumberStyles, CultureInfo.CurrentCulture, out d);
                    return result;



                default: throw new ArgumentException("Unknown TextBoxInputMode");

            }
        }

        private bool CheckIsDigit(string wert)
        {
            return wert.ToCharArray().All(Char.IsDigit);
        }
    }

    public enum TextBoxInputMode
    {
        None,
        DecimalInput,
        DigitInput
    }
}
