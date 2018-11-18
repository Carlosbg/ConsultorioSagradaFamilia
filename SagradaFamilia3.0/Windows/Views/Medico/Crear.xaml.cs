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
    /// <summary>
    /// Lógica de interacción para Crear.xaml
    /// </summary>
    public partial class Crear : Page
    {
        public Crear()
        {
            InitializeComponent();
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            Medicos medicos = new Medicos();
            Layout.Frame.Navigate(medicos);
        }

        private void ButtonCrear_Click(object sender, RoutedEventArgs e)
        {
            if(Apellido.Text == "")
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
                Telefono = int.Parse(Telefono.Text)
            };

            StatusMessage statusMessage = DbContextSingleton.dbContext.GuardarMedico(medico);

            MessageBox.Show(statusMessage.Mensaje);

            if(statusMessage.Status == 0)
            {
                Medicos medicos2 = new Medicos();
                Layout.Frame.Navigate(medicos2);
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
