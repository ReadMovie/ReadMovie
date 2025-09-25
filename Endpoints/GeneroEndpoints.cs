using Microsoft.EntityFrameworkCore;
using ReadMovie.Data;
using ReadMovie.Dto;
using ReadMovie.Models;

namespace ReadMovie.Endpoints
{
    public static class GeneroEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/generos").WithTags("Generos");

            group.MapPost("/", async (ReadMovieDb db, CrearGeneroDto dto) => {
                var errores = new Dictionary<string, string[]>();

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    errores["nombre"] = ["El nombre es requerido."];

                if (errores.Count > 0) return Results.ValidationProblem(errores);

                var entity = new Genero
                {
                    Nombre = dto.Nombre
                };

                db.Generos.Add(entity);
                await db.SaveChangesAsync();

                var dtoSalida = new GeneroDto(
                    entity.Id,
                    entity.Nombre);

                return Results.Created($"/generos/{entity.Id}", dtoSalida);

            });

            group.MapGet("/", async (ReadMovieDb db) => {

                var consulta = await db.Generos.ToListAsync();

                var generos = consulta.Select(c => new GeneroDto(
                    c.Id,
                    c.Nombre
                ))
                .OrderBy(c => c.Nombre)
                .ToList();

                return Results.Ok(generos);

            });

            group.MapGet("/{id}", async (short id, ReadMovieDb db) => {
                var genero = await db.Generos
                .Where(c => c.Id == id)
                .Select(c => new GeneroDto(
                    c.Id,
                    c.Nombre
                )).FirstOrDefaultAsync();

                return Results.Ok(genero);
            });

            group.MapPut("/{id}", async (short id, ModificarGeneroDto dto, ReadMovieDb db) => {
                var genero = await db.Generos.FindAsync(id);

                if (genero is null)
                    return Results.NotFound();

                genero.Nombre = dto.Nombre;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (short id, ReadMovieDb db) =>
            {
                var genero = await db.Generos.FindAsync(id);

                if (genero is null)
                    return Results.NotFound();

                db.Remove(genero);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
