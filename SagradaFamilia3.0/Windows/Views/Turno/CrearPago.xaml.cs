using ConsultorioSagradaFamilia.Models;
using SagradaFamilia3._0.Models;
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
    public enum FormasPago
    {
        Contado = 1,
        ObraSocial = 2,
        Debito = 3
    }
    /// <summary>
    /// Lógica de interacción para CrearPago.xaml
    /// </summary>
    public partial class CrearPago : Page
    {
        public int IdTurno { get; set; }
        public int IdPaciente { get; set; }
        public string NombreMedico { get; set; }
        public string NombrePaciente { get; set; }

        public CrearPago(int idTurno, string nombreMedico, string nombrePaciente, int idPaciente)
        {
            InitializeComponent();
            IdTurno = idTurno;
            NombreMedico = nombreMedico;
            NombrePaciente = nombrePaciente;
            IdPaciente = idPaciente;

            MedicoTextBox.Text = nombreMedico;
            PacienteTextBox.Text = nombrePaciente;
        }

        private void FormaPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((int)FormaPagoCombobox.SelectedValue == (int)FormasPago.Contado)
            {
                TarjetaCombobox.IsEnabled = false;
                ObraSocialCombobox.IsEnabled = false;
            }
            else if ((int)FormaPagoCombobox.SelectedValue == (int)FormasPago.ObraSocial)
            {
                ObraSocialCombobox.IsEnabled = true;
                TarjetaCombobox.IsEnabled = false;
                FillObrasSociales();
            }
            else if ((int)FormaPagoCombobox.SelectedValue == (int)FormasPago.Debito)
            {
                TarjetaCombobox.IsEnabled = true;
                ObraSocialCombobox.IsEnabled = false;
                FillTarjetas();
            }
        }

        private void FormaPagoCombobox_Initialized(object sender, EventArgs e)
        {
            FormaPagoCombobox.ItemsSource = DbContextSingleton.dbContext.GetFormasPago();

            FormaPagoCombobox.DisplayMemberPath = "Nombre";
            FormaPagoCombobox.SelectedValuePath = "IdFormaPago";
        }

        private void FillTarjetas()
        {
            List<ConsultorioSagradaFamilia.Models.Tarjeta> tarjetaList = DbContextSingleton.dbContext.GetTarjetasPorPaciente(IdPaciente);

            TarjetaCombobox.ItemsSource = tarjetaList;
            TarjetaCombobox.DisplayMemberPath = "Nombre";
            TarjetaCombobox.SelectedValuePath = "IdTarjeta";
        }

        private void FillObrasSociales()
        {
            List<ConsultorioSagradaFamilia.Models.ObraSocial> obraSocialList = DbContextSingleton.dbContext.GetObrasSocialesPorPaciente(IdPaciente);

            ObraSocialCombobox.ItemsSource = obraSocialList;
            ObraSocialCombobox.DisplayMemberPath = "Nombre";
            ObraSocialCombobox.SelectedValuePath = "IdObraSocial";
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            Turnos turnos = new Turnos();
            Layout.Frame.Navigate(turnos);
        }

        private void ButtonCrear_Click(object sender, RoutedEventArgs e)
        {
            if (Monto.Text == "")
            {
                MessageBox.Show("Debe indicar un monto");
                return;
            }

            if (FormaPagoCombobox.SelectedValue == null)
            {
                MessageBox.Show("Debe indicar una forma de pago");
                return;
            }
            else
            {
                if ((int)FormaPagoCombobox.SelectedValue == (int)FormasPago.ObraSocial)
                {
                    if (ObraSocialCombobox.SelectedValue == null)
                    {
                        MessageBox.Show("Debe indicar una obra social");
                        return;
                    }
                }
                else if ((int)FormaPagoCombobox.SelectedValue == (int)FormasPago.Debito)
                {
                    if (TarjetaCombobox.SelectedValue == null)
                    {
                        MessageBox.Show("Debe indicar una tarjeta");
                        return;
                    }
                }
            }

            Pago pago = new Pago
            {
                IdFormaPago = int.Parse(FormaPagoCombobox.SelectedValue.ToString()),
                IdTurno = IdTurno,
                Monto = decimal.Parse(Monto.Text)                
            };

            if ((int)FormaPagoCombobox.SelectedValue == (int)FormasPago.ObraSocial)
            {
                pago.IdObraSocial = int.Parse(ObraSocialCombobox.SelectedValue.ToString());
            }
            else if ((int)FormaPagoCombobox.SelectedValue == (int)FormasPago.Debito)
            {
                pago.IdTarjeta = int.Parse(TarjetaCombobox.SelectedValue.ToString());
            }

            StatusMessage statusMessage = DbContextSingleton.dbContext.GuardarPago(pago);

            MessageBox.Show(statusMessage.Mensaje);

            if (statusMessage.Status == 0)
            {
                DbContextSingleton.dbContext.MarcarTurnoAtendido(IdTurno);

                Turnos turnos = new Turnos();
                Layout.Frame.Navigate(turnos);
            }
        }
    }
}
