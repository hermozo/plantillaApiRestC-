namespace apirest.Model
{
    public class Compra
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public virtual Libro Libro { get; set; }

    }
}
