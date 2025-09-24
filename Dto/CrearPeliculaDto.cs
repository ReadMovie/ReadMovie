namespace ReadMovie.Dto
{
    public record CrearPeliculaDto
    (
      short CategoriaId,
      short GeneroId,
      string Titulo,
      string Director,
      DateTime FechaLanzamiento,
      string Resumen
        
    );
}
