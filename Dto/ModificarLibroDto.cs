namespace ReadMovie.Dto
{
    public record ModificarLibroDto
    (
        short CategoriaId,
        short GeneroId,
        string Titulo,
        string Autor,
        DateTime FechaPublicacion,
        string Resumen
    );
}
