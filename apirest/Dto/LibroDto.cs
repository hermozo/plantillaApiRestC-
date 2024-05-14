namespace apirest.Dto
{
    public class LibroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Editorial { get; set; }
        public CategoriaDto Categoria { get; set; }
    }
}