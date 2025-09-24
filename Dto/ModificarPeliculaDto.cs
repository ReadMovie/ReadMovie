namespace ReadMovie.Dto
{
    public record ModificarPeliculaDto
   (
      short CategoriaId,
      short GeneroId,
      string Titulo,
      string Director,
      DateTime FechaLanzamiento,
      string Resumen

   );
}
