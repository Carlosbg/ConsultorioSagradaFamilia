namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public  class Especialidad
    {
        public int IdEspecialidad { get; set; }

        public string Nombre { get; set; }

        public bool Habilitada { get; set; }

        //public virtual ICollection<MedicoEspecialidad> MedicoEspecialidad { get; set; }
    }
}
