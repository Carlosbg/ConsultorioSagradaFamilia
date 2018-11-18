namespace ConsultorioSagradaFamilia.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Usuario
    {
        public string Email { get; set; }
        public string Id { get; set; }
        public string Rol { get; set; }
        public string Password { get; set; }
    }
}
