namespace ReadMovie.Dto
{
    public record ModificarUsuarioDto
    (
        string Nombre,
        DateTime FechaNacimiento,
        string Email,
        string Clave,
        int Rol
    );
}
