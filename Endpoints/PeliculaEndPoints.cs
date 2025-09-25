using Microsoft.EntityFrameworkCore;
using ReadMovie.Data;
using ReadMovie.Dto;
using ReadMovie.Models;

namespace ReadMovie.Endpoints
{
    public static class PeliculaEndPoints
    {
        public static void Add(this IEndpointRouteBuilder routes) {
            var group = routes.MapGroup("/api/peliculas").WithTags("Peliculas");

            group.MapPost("/", async (ReadMovieDb db, CrearPeliculaDto dto) => {
                var errores = new Dictionary<string, string[]>();

                if (string.IsNullOrWhiteSpace(dto.Titulo))
                    errores["titulo"] = ["El titulo es requerido."];

                if (string.IsNullOrWhiteSpace(dto.Director))
                    errores["director"] = ["El director es requerido."];

                var entity = new Pelicula {
                    GeneroId = dto.GeneroId,
                    CategoriaId = dto.CategoriaId,
                    Titulo = dto.Titulo,
                    Director = dto.Director,
                    FechaLanzamineto = DateTime.SpecifyKind(dto.FechaLanzamiento, DateTimeKind.Utc),
                    Resumen = dto.Resumen,
                };

                db.Peliculas.Add(entity);
                await db.SaveChangesAsync();

                var dtoSalida = new PeliculaDto( entity.Id,
                    entity.CategoriaId,
                    entity.GeneroId, 
                    entity.Titulo,
                    entity.Director,
                    entity.FechaLanzamineto,
                    entity.Resumen);

                return Results.Created($"/peliculas/{entity.Id}", dtoSalida);
            });

            group.MapGet("/", async (ReadMovieDb db) => {

                var consulta = await db.Peliculas.ToListAsync();

                var peliculas = consulta.Select(p => new PeliculaDto(
                    p.Id,
                    p.CategoriaId,
                    p.GeneroId,
                    p.Titulo,
                    p.Director,
                    p.FechaLanzamineto,
                    p.Resumen

                    ))
                    .OrderBy(p => p.Titulo)
                    .ToList();

                return Results.Ok(peliculas);
            });

            group.MapGet("/{id}", async (long id, ReadMovieDb db) =>
            {
                var pelicula = await db.Peliculas
                .Where(p => p.Id == id)
                .Select(p => new PeliculaDto(
                    p.Id,
                    p.GeneroId,
                    p.CategoriaId,
                    p.Titulo,
                    p.Director,
                    p.FechaLanzamineto,
                    p.Resumen
                )).FirstOrDefaultAsync();

                if (pelicula is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(pelicula);
            });

            group.MapPut("/{id}", async (long id, ModificarPeliculaDto dto, ReadMovieDb db) =>
            {
                var pelicula = await db.Peliculas.FindAsync(id);

                if (pelicula is null)
                    return Results.NotFound();

                pelicula.GeneroId = dto.GeneroId;
                pelicula.CategoriaId = dto.CategoriaId;
                pelicula.Titulo = dto.Titulo;
                pelicula.Director = dto.Director;
                pelicula.FechaLanzamineto = DateTime.SpecifyKind(dto.FechaLanzamiento, DateTimeKind.Utc);
                pelicula.Resumen = dto.Resumen;
                
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (long id, ReadMovieDb db) =>
            {
                var pelicula = await db.Peliculas.FindAsync(id);

                if (pelicula is null)
                    return Results.NotFound();

                db.Remove(pelicula);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
       
    }
}
