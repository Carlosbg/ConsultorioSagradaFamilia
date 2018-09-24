namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class HorarioAtencion
    {
        public int IdHorarioAtencion { get; set; }

        public int IdMedico { get; set; }

        public byte IdDia { get; set; }

        public TimeSpan HorarioInicio { get; set; }

        public TimeSpan HorarioFinal { get; set; }

        public bool Habilitado { get; set; }

        //public virtual Medico Medico { get; set; }
        //public virtual ICollection<Turno> Turno { get; set; }
    }
}
