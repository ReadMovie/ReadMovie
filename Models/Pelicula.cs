namespace ReadMovie.Models
{
    public class Pelicula
    {
        public long Id { get; set; }
        public short CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public short GeneroId { get; set; }
        public Genero? Genero { get; set; }
        public string Titulo {  get; set; }
        public string Director {  get; set; }
        public DateTime FechaLanzamineto {  get; set; }
        public string Resumen {  get; set; }

        public List<Criterio> Criterios { get; set; } = new();
    }
}
