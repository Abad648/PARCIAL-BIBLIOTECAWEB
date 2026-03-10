namespace Biblioteca.Models
{
    public class Libro
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public int Publicacion { get; set; }

        public int IdAutor { get; set; }

        public int IdCategoria { get; set; }

        public string Autor { get; set; }

        public string Categoria { get; set; }
    }
}