namespace ReadMovie.Dto
{
    public record CrearLibroDto
    (
        short CategoriaId,
        short GeneroId,
        string Titulo,
        string Autor,
        DateTime FechaPublicacion,
        string Resumen
    );
}
