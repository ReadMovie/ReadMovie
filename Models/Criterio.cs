namespace ReadMovie.Models
{
    public class Criterio
    {
        public long Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public long? PeliculaId { get; set; }
        public Pelicula? Pelicula { get; set; }
        public long? LibroId { get; set; }
        public Libro? Libro { get; set; }
        public string? Comentario {  get; set; }
        public DateTime Fecha { get; set; }
        public short Puntuacion { get; set; }
    }
}
