﻿using SagradaFamilia3._0.Models;
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

namespace SagradaFamilia3._0.Windows.Views.ObraSocial
{
    /// <summary>
    /// Lógica de interacción para CrearObraSocial.xaml
    /// </summary>
    public partial class CrearObraSocial : Page
    {
        public CrearObraSocial()
        {
            InitializeComponent();
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            ObrasSociales obrasSociales = new ObrasSociales();
            Layout.Frame.Navigate(obrasSociales);
        }

        private void ButtonCrear_Click(object sender, RoutedEventArgs e)
        {
            if (Nombre.Text == "")
            {
                MessageBox.Show("Debe indicar un Nombre");
                return;
            }

            ConsultorioSagradaFamilia.Models.ObraSocial obraSocial = new ConsultorioSagradaFamilia.Models.ObraSocial
            {
                Nombre = Nombre.Text,
                Habilitada = true
            };

            StatusMessage statusMessage = DbContextSingleton.dbContext.GuardarObraSocial(obraSocial);

            MessageBox.Show(statusMessage.Mensaje);

            if (statusMessage.Status == 0)
            {
                ObrasSociales obrasSociales = new ObrasSociales();
                Layout.Frame.Navigate(obrasSociales);
            }
        }
    }
}
