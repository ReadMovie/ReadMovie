namespace ReadMovie.Dto
{
    public record CriterioDto
    (
        long Id,
        int UsuarioId,
        long? PeliculaId,
        long? LibroId,
        DateTime Fecha,
        string? Comentario,
        short Puntuacion
    );
}
