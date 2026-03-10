using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaWeb.Models
{
    public class Autor
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Nacionalidad { get; set; }

        public DateTime? FechaNacimiento { get; set; }
    }
}