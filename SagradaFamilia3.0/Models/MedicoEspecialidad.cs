namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class MedicoEspecialidad
    {
        public int IdMedicoEspecialidad { get; set; }

        public int IdMedico { get; set; }

        public int IdEspecialidad { get; set; }

        //public virtual Medico Medico { get; set; }
        //public virtual Especialidad Especialidad { get; set; }
    }
}
