namespace ConsultorioSagradaFamilia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PacientesAtendidos
    {
        public DateTime Fecha { get; set; }

        public string NombrePaciente { get; set; }

        public string NombreMedico { get; set; }

        public int IdPaciente { get; set; }

        public int IdMedico { get; set; }
    }
}
