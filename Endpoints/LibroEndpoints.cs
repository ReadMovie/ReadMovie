using Microsoft.EntityFrameworkCore;
using ReadMovie.Data;
using ReadMovie.Dto;
using ReadMovie.Models;
using System.Runtime.CompilerServices;

namespace ReadMovie.Endpoints
{
    public static class LibroEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/libros").WithTags("Libros");

            group.MapPost("/", async (ReadMovieDb db, CrearLibroDto dto) => {
                var errores = new Dictionary<string, string[]>();

                if (string.IsNullOrWhiteSpace(dto.Titulo))
                    errores["titulo"] = ["El titulo es requerido."];

                if (string.IsNullOrWhiteSpace(dto.Autor))
                    errores["autor"] = ["El autor es requerido."];

                if (dto.FechaPublicacion == default)
                    errores["fechapublicacion"] = ["La fecha de publicacion es requerida."];

                if (errores.Count > 0) return Results.ValidationProblem(errores);

                var entity = new Libro
                {
                    Titulo = dto.Titulo,
                    Autor = dto.Autor,
                    FechaPublicacion = dto.FechaPublicacion,
                    Resumen = dto.Resumen,
                };

                db.Libros.Add(entity);
                await db.SaveChangesAsync();

                // Se inicializan CategoriaId y GeneroId a 0, ya que no existen en la entidad Libro
                var dtoSalida = new LibroDto(
                    entity.Id,
                    0, // Valor por defecto para CategoriaId
                    0, // Valor por defecto para GeneroId
                    entity.Titulo,
                    entity.Autor,
                    entity.FechaPublicacion,
                    entity.Resumen);

                return Results.Created($"/libros/{entity.Id}", dtoSalida);
            });

            group.MapGet("/", async (ReadMovieDb db) => {
                var consulta = await db.Libros.ToListAsync();

                var libros = consulta.Select(l => new LibroDto(
                    l.Id,
                    0, // Valor por defecto para CategoriaId
                    0, // Valor por defecto para GeneroId
                    l.Titulo,
                    l.Autor,
                    l.FechaPublicacion,
                    l.Resumen
                ))
                .OrderBy(l => l.Titulo)
                .ToList();

                return Results.Ok(libros);
            });

            group.MapGet("/{id}", async (int id, ReadMovieDb db) =>
            {
                var libro = await db.Libros
                .Where(l => l.Id == id)
                .Select(l => new LibroDto(
                    l.Id,
                    0,
                    0, 
                    l.Titulo,
                    l.Autor,
                    l.FechaPublicacion,
                    l.Resumen
                )).FirstOrDefaultAsync();

                if (libro is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(libro);
            });

            group.MapPut("/{id}", async (int id, ModificarLibroDto dto, ReadMovieDb db) =>
            {
                var libro = await db.Libros.FindAsync(id);

                if (libro is null)
                    return Results.NotFound();

                libro.Titulo = dto.Titulo;
                libro.Autor = dto.Autor;
                libro.FechaPublicacion = dto.FechaPublicacion;
                libro.Resumen = dto.Resumen;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}