namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class MedicoFormaPago
    {
        public int IdMedicoFormaPago { get; set; }

        public int IdMedico { get; set; }

        public int IdFormaPago { get; set; }

        //public virtual Medico Medico { get; set; }
        //public virtual FormaPago FormaPago { get; set; }
    }
}
