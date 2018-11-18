using ConsultorioSagradaFamilia.Models;
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
    /// Lógica de interacción para Especialidades.xaml
    /// </summary>
    public partial class Especialidades : Page
    {
        public List<Especialidad> especialidades = new List<Especialidad>();

        public Especialidades()
        {
            InitializeComponent();

            especialidades = DbContextSingleton.dbContext.GetEspecialidades();
            EspecialidadesGrid.ItemsSource = especialidades;
            EspecialidadesGrid.IsReadOnly = true;
        }
    }
}
