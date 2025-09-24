namespace ReadMovie.Dto
{
    public record ModificarCriterioDto
    (
        int UsuarioId,
        long? PeliculaId,
        long? LibroId,
        DateTime Fecha,
        string Comentario,
        short Puntuacion
    );
}
