namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class HorarioAtencion
    {
        public string Dia
        {
            get
            {
                if (IdDia == 1) return "Lunes";
                if (IdDia == 2) return "Martes";
                if (IdDia == 3) return "Miércoles";
                if (IdDia == 4) return "Jueves";
                if (IdDia == 5) return "Viernes";
                if (IdDia == 6) return "Sábado";
                else return "Domingo";
            }
        }

        public int IdHorarioAtencion { get; set; }

        public int IdMedico { get; set; }

        public int IdDia { get; set; }

        public TimeSpan HorarioInicio { get; set; }

        public TimeSpan HorarioFinal { get; set; }

        public bool Habilitado { get; set; }
       
        //public virtual Medico Medico { get; set; }
        //public virtual ICollection<Turno> Turno { get; set; }
    }
}
