namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ObraSocialPaciente
    {
        public int IdObraSocialPaciente { get; set; }

        public int IdObraSocial { get; set; }

        public int IdPaciente { get; set; }

        public bool Habilitado { get; set; }

        //public virtual ObraSocial ObraSocial { get; set; }
        //public virtual Paciente Paciente { get; set; }
    }
}
