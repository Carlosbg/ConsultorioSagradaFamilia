namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PacienteTarjeta
    {
        public int IdPacienteTarjeta{ get; set; }

        public int IdPaciente { get; set; }

        public int IdTarjeta { get; set; }
    }
}
