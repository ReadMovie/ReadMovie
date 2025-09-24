using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ReadMovie.Data;
using ReadMovie.Dto;
using ReadMovie.Models;
using System.Threading.Tasks;

namespace ReadMovie.Endpoints
{
    public static class UsuarioEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/usuarios").WithTags("usuarios");

            group.MapPost("/", async (ReadMovieDb db, CrearUsuarioDto dto) =>
            {
                var errores = new Dictionary<string, string[]>();

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    errores["nombre"] = ["El nomnbre es requerido."];

                if (dto.FechaNacimiento == DateTime.MinValue)
                    errores["fechaNacimiento"] = new[] { "La fecha de nacimiento es requerido." };

                if (string.IsNullOrWhiteSpace(dto.Email))
                    errores["email"] = ["El email es requerido"];

                if (string.IsNullOrWhiteSpace(dto.Clave))
                    errores["clave"] = ["La clave es requerida"];

                if (errores.Count > 0) return Results.ValidationProblem(errores);

                var entity = new Usuario
                {
                    Nombre = dto.Nombre,
                    FechaNacimiento = dto.FechaNacimiento,
                    Email = dto.Email,
                    Clave = dto.Clave,

                };

                db.Usuarios.Add(entity);
                await db.SaveChangesAsync();

                var dtoSalida = new UsuarioDto(
                    entity.Id,
                    entity.Nombre,
                    entity.FechaNacimiento,
                    entity.Email,
                    entity.Clave,
                    entity.Rol);
                return Results.Created($"/usuario/{entity.Id}", dtoSalida);
            });

            group.MapGet("/", async (ReadMovieDb db) => {
                var consulta = await db.Usuarios.ToListAsync();

                var Usuarios = consulta.Select(u => new UsuarioDto(
                    u.Id,
                    u.Nombre,
                    u.FechaNacimiento,
                    u.Email,
                    u.Clave,
                    u.Rol
                ))
                .OrderBy(u => u.Id)
                .ToList();
                return Results.Ok(Usuarios);

            });

            group.MapGet("/{id}", async (int id, ReadMovieDb db) => {
                var usuario = await db.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new UsuarioDto(
                    u.Id,
                    u.Nombre,
                    u.FechaNacimiento,
                    u.Email,
                    u.Clave,
                    u.Rol
                    )).FirstOrDefaultAsync();

                return Results.Ok(usuario);
            });

            group.MapPut("/{id}", async (int id, ModificarUsuarioDto dto, ReadMovieDb db) => {
                var Usuario = await db.Usuarios.FindAsync(id);
                if (Usuario is null)
                    return Results.NotFound();

                Usuario.Nombre = dto.Nombre;
                Usuario.FechaNacimiento = dto.FechaNacimiento;
                Usuario.Email = dto.Email;
                Usuario.Clave = dto.Clave;
                Usuario.Rol = dto.Rol;

                await db.SaveChangesAsync();

                return Results.NoContent();
 
            });
        }
    }
}
