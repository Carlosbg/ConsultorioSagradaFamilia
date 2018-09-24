namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ObraSocialMedico
    {
        public int IdObraSocialMedico { get; set; }

        public int IdObraSocial { get; set; }

        public int IdMedico { get; set; }

        public bool Habilitado { get; set; }
       
        //public virtual Medico Medico { get; set; }
        //public virtual ObraSocial ObraSocial { get; set; }
    }
}
