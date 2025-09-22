namespace ReadMovie.Dto
{
    public record UsuarioDto
    (
        long Id,
        string Nombre,
        DateTime FechaNacimiento,
        string Email,
        string Clave,
        int Rol
    );
    
}
