namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Paciente
    {
        public int IdPaciente { get; set; }

        public int DNI { get; set; }
        
        public string Nombre { get; set; }
       
        public string Apellido { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }

        public string ApellidoNombre => Apellido + ", " + Nombre;

        //public virtual ICollection<HistoriaClinica> HistoriaClinica { get; set; }       
        //public virtual ICollection<ObraSocialPaciente> ObraSocialPaciente { get; set; }        
        //public virtual ICollection<PacienteMedico> PacienteMedico { get; set; }     
        //public virtual ICollection<Turno> Turno { get; set; }
    }
}
