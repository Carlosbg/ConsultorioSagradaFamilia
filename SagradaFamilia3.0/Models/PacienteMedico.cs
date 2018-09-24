namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PacienteMedico
    {
        public int IdPacienteMedico { get; set; }

        public int IdPaciente { get; set; }

        public int IdMedico { get; set; }

        public bool Habilitado { get; set; }

        //public virtual Medico Medico { get; set; }
        //public virtual Paciente Paciente { get; set; }
    }
}
