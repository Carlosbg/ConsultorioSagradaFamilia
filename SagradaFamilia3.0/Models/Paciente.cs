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

        public string ApellidoNombre => Nombre != "[Ninguno]" ? Apellido + ", " + Nombre : Nombre;

        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }       

        public string FechaNacimientoString => FechaNacimiento.Day + "/" + FechaNacimiento.Month + "/" +
                                               FechaNacimiento.Year;

        public string Email { get; set; }
        //public virtual ICollection<HistoriaClinica> HistoriaClinica { get; set; }       
        //public virtual ICollection<ObraSocialPaciente> ObraSocialPaciente { get; set; }        
        //public virtual ICollection<PacienteMedico> PacienteMedico { get; set; }     
        //public virtual ICollection<Turno> Turno { get; set; }
    }
}
