namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Tarjeta
    {
        public int IdTarjeta { get; set; }

        public string Nombre{ get; set; }

        public string Numero { get; set; }

        public int IdBanco { get; set; }

        //public virtual Banco Banco { get; set; }
    }
}
