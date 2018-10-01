namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Pago
    {
        public int IdPago { get; set; }

        public int? IdObraSocial { get; set; }

        public int IdFormaPago { get; set; }

        public int IdTurno { get; set; }

        public decimal Monto { get; set; }

        public string NumeroTarjeta { get; set; }

        //public virtual FormaPago FormaPago { get; set; }
        //public virtual ObraSocial ObraSocial { get; set; }
        //public virtual Turno Turno { get; set; }
    }
}
