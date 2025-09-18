namespace ReadMovie.Models
{
    public class Genero
    {
        public short Id { get; set; }
        public string Nombre { get; set; }

        public List<Libro> Libros { get; set; } = new();
        public List<Pelicula> Peliculas { get; set; } = new();
    }
}
