namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SolicitudOrden
    {
        public int IdSolicitudOrden { get; set; }

        public string Mensaje { get; set; }

        public int IdPaciente { get; set; }

        public int IdMedico { get; set; }

        public string Orden { get; set; }
    }

    public partial class SolicitudOrdenView
    {
        public int IdSolicitudOrden { get; set; }

        public string Mensaje { get; set; }

        public int IdPaciente { get; set; }

        public string PacienteNombre { get; set; }

        public int IdMedico { get; set; }

        public string MedicoNombre { get; set; }

        public string Orden { get; set; }
    }
}
