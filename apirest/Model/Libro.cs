using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apirest.Model
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public string Editorial { get; set; }
        public string Genero { get; set; }
        public int AnioPublicacion { get; set; }
        public int Paginas { get; set; }
        public string Idioma { get; set; }
        public string Resumen { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public bool Disponibilidad { get; set; }
        public string ImagenPortada { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
    }
}
