namespace ReadMovie.Dto
{
    public record ModificarCriterioDto
    (
        int UsuarioId,
        long? PeliculaId,
        long? LibroId,
        string Comentario,
        short Puntuacion
    );
}
