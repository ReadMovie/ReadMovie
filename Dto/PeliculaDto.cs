namespace ReadMovie.Dto
{
    public record PeliculaDto
    (
      long Id,
      short CategoriaId,
      short GeneroId,
      string Titulo,
      string Director,
      DateTime FechaLanzamiento,
      string Resumen

   );
}
