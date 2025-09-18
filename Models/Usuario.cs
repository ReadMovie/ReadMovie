using Microsoft.AspNetCore.Identity;

namespace ReadMovie.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        //public Rol Rol { get; set; }
        public int Rol { get; set; }


        public List<Criterio> Criterios { get; set; } = new();
    }
    public enum Rol
    {
        Usuario = 0,
        Administrador = 1
    }
}
