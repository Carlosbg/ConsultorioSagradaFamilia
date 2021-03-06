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

namespace SagradaFamilia3._0.Windows.Views.Banco
{
    /// <summary>
    /// Lógica de interacción para EditarBanco.xaml
    /// </summary>
    public partial class EditarBanco : Page
    {
        public ConsultorioSagradaFamilia.Models.Banco Banco { get; set; }

        public EditarBanco(ConsultorioSagradaFamilia.Models.Banco banco)
        {
            InitializeComponent();
            Banco = banco;

            Nombre.Text = banco.Nombre;
        }

        private void ButtonVolver_Click(object sender, RoutedEventArgs e)
        {
            Bancos bancos = new Bancos();
            Layout.Frame.Navigate(bancos);
        }

        private void ButtonEditar_Click(object sender, RoutedEventArgs e)
        {
            if (Nombre.Text == "")
            {
                MessageBox.Show("Debe indicar un Nombre");
                return;
            }

            ConsultorioSagradaFamilia.Models.Banco banco = new ConsultorioSagradaFamilia.Models.Banco
            {
                IdBanco = Banco.IdBanco,
                Nombre = Nombre.Text
            };

            StatusMessage statusMessage = DbContextSingleton.dbContext.EditarBanco(banco);

            MessageBox.Show(statusMessage.Mensaje);

            if (statusMessage.Status == 0)
            {
                Bancos bancos = new Bancos();
                Layout.Frame.Navigate(bancos);
            }
        }
    }
}
