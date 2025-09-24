namespace ReadMovie.Dto
{
    public record CrearCriterioDto
    (
        int UsuarioId,
        long? PeliculaId,
        long? LibroId,
        DateTime Fecha,
        string Comentario,
        short Puntuacion
    );
}
