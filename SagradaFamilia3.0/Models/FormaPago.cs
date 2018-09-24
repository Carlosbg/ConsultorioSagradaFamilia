namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class FormaPago
    {
        public int IdFormaPago { get; set; }

        public string Nombre { get; set; }

        //public virtual ICollection<Pago> Pago { get; set; }
        //public virtual ICollection<MedicoFormaPago> MedicoFormaPago { get; set; }
    }
}
