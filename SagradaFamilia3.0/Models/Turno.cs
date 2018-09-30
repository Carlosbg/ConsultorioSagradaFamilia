namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Turno
    {
        public int IdTurno { get; set; }

        public int? IdHorarioAtencion { get; set; }

        public int IdPaciente { get; set; }

        public int IdMedico { get; set; }

        public DateTime Fecha { get; set; }

        public bool Atendido { get; set; }

        public int? Orden { get; set; }

        //public virtual HorarioAtencion HorarioAtencion { get; set; }
        //public virtual Medico Medico { get; set; }
        //public virtual Paciente Paciente { get; set; }
        //public virtual ICollection<Pago> Pago { get; set; }
    }
}
