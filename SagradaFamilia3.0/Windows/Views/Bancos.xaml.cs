﻿using ConsultorioSagradaFamilia.Models;
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

namespace SagradaFamilia3._0.Windows.Views
{
    /// <summary>
    /// Lógica de interacción para Bancos.xaml
    /// </summary>
    public partial class Bancos : Page
    {
        public List<Banco> bancos = new List<Banco>();

        public Bancos()
        {
            InitializeComponent();

            bancos = DbContextSingleton.dbContext.GetBancos();
            BancosGrid.ItemsSource = bancos;
            BancosGrid.IsReadOnly = true;
        }
    }
}
