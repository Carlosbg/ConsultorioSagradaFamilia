namespace ConsultorioSagradaFamilia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PagosPorFormaPago
    {
        public string FormaPago { get; set; }

        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        public string NombreMedico { get; set; }

        public string NombrePaciente { get; set; }

        public int IdMedico { get; set; }

        public int IdObraSocial { get; set; }

        public string NombreObraSocial{ get; set; }

        public int IdFormaPago { get; set; }
    }
}
