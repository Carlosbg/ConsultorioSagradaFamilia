namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Medico
    {
        public int IdMedico { get; set; }

        public int DNI { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string ApellidoNombre => Nombre != "[Ninguno]" ? Apellido + ", " + Nombre : Nombre;

        public int Matricula { get; set; }

        public string CUIL { get; set; }

        public decimal Monto { get; set; }       

        public string Domicilio { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Mail { get; set; }

        public int Telefono { get; set; }

        //public virtual ICollection<HistoriaClinica> HistoriaClinica { get; set; }
        //public virtual ICollection<HorarioAtencion> HorarioAtencion { get; set; }
        //public virtual ICollection<ObraSocialMedico> ObraSocialMedico { get; set; }
        //public virtual ICollection<PacienteMedico> PacienteMedico { get; set; }
        //public virtual ICollection<Turno> Turno { get; set; }
        //public virtual ICollection<MedicoEspecialidad> MedicoEspecialidad { get; set; }
        //public virtual ICollection<MedicoFormaPago> MedicoFormaPago { get; set; }
    }
}
