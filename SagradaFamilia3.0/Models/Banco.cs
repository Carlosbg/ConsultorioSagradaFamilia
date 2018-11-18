namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Banco
    {
        public int IdBanco { get; set; }

        public string Nombre { get; set; }

    }
}
