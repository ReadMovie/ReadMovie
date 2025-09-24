using Microsoft.EntityFrameworkCore;
using ReadMovie.Data;
using ReadMovie.Dto;
using ReadMovie.Models;

namespace ReadMovie.Endpoints
{
    public static class CriterioEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes) {
            var group = routes.MapGroup("/api/criterios").WithTags("Criterios");

            group.MapPost("/", async (ReadMovieDb db, CrearCriterioDto dto) => {
                var errores = new Dictionary<string, string[]>();

                if (string.IsNullOrWhiteSpace(dto.Comentario))
                    errores["Comentario"] = ["El Comentario es requerido"];

                if (dto.Puntuacion < 0)
                    errores["puntuaciones"] = ["Las puntuaciones no pueden ser Negativos. "];

                if (errores.Count > 0) return Results.ValidationProblem(errores);

                var entity = new Criterio {
                    UsuarioId = dto.UsuarioId,
                    PeliculaId = dto.PeliculaId,
                    LibroId = dto.LibroId,
                    Comentario = dto.Comentario,
                    Fecha = dto.Fecha,
                    Puntuacion = dto.Puntuacion,
                };

                db.Criterios.Add(entity);
                await db.SaveChangesAsync();

                var dtoSalida = new CriterioDto(
                    entity.Id,
                    entity.UsuarioId,
                    entity.PeliculaId,
                    entity.LibroId,
                    entity.Fecha,
                    entity.Comentario,
                    entity.Puntuacion,
                    0,
                    5
                );

            return Results.Created($"/criterios/{entity.Id}", dtoSalida);

            });

            group.MapGet("/", async (ReadMovieDb db) => {

                var consulta = await db.Criterios.ToListAsync();

                var criterios = consulta.Select(c => new CriterioDto(
                    c.Id,
                    c.UsuarioId,
                    c.PeliculaId,
                    c.LibroId,
                    c.Fecha,
                    c.Comentario,
                    c.Puntuacion,
                    default,
                    default
                ))
                .OrderBy(c => c.Comentario)
                .ToList();

                return Results.Ok(criterios);   
            });
        }
    }
}
