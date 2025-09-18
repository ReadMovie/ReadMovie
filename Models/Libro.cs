namespace ReadMovie.Models
{
    public class Libro
    {
        public long Id { get; set; }
        public short CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public short GeneroId { get; set; }
        public Genero? Genero { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Resumen { get; set; }

        public List<Criterio> Criterios { get; set; } = new();
    }
}
