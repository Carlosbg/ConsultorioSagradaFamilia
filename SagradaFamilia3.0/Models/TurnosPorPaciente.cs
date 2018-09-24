namespace ConsultorioSagradaFamilia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TurnosPorPaciente
    {
        public DateTime Fecha { get; set; }

        public string NombrePaciente { get; set; }

        public int IdMedico { get; set; }

        public int IdPaciente { get; set; }

        public string NombreMedico { get; set; }

        public int DNIMedico { get; set; }

        public int MatriculaMedico { get; set; }

        public string CUILMedico { get; set; }
    }
}
