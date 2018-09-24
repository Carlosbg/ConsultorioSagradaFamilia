namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ObraSocial
    {
        public int IdObraSocial { get; set; }

        public string Nombre { get; set; }

        //public virtual ICollection<ObraSocialMedico> ObraSocialMedico { get; set; }
        //public virtual ICollection<ObraSocialPaciente> ObraSocialPaciente { get; set; }
        //public virtual ICollection<Pago> Pago { get; set; }
    }
}
