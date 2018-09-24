namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class HistoriaClinica
    {
        public int IdHistoriaClinica { get; set; }

        public int IdMedico { get; set; }

        public int IdPaciente { get; set; }

        public DateTime Fecha { get; set; }

        public string Observaciones { get; set; }

        //public virtual Medico Medico { get; set; }
        //public virtual Paciente Paciente { get; set; }
    }
}
