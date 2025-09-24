using Microsoft.EntityFrameworkCore;
using ReadMovie.Data;
using ReadMovie.Dto;
using ReadMovie.Models;
using System.Threading.Tasks.Dataflow;

namespace ReadMovie.Endpoints
{
    public static class CategoriaEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/categorias").WithTags("Categorias");

            group.MapPost("/", async (ReadMovieDb db, CrearCategoriaDto dto) => { 
               var errores = new Dictionary<string, string[]>();

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    errores["nombre"] = ["El nombre es requerido."];

                if (errores.Count > 0) return Results.ValidationProblem(errores);

                var entity = new Categoria
                {
                    Nombre = dto.Nombre
                };

                db.Categorias.Add(entity);
                await db.SaveChangesAsync();

                var dtoSalida = new CategoriaDto(
                    entity.Id, 
                    entity.Nombre);

                return Results.Created($"/categorias/{entity.Id}", dtoSalida);

            });

            group.MapGet("/", async (ReadMovieDb db) => {

                var consulta = await db.Categorias.ToListAsync();

                var categorias = consulta.Select(c => new CategoriaDto(
                    c.Id,
                    c.Nombre
                ))
                .OrderBy(c => c.Nombre)
                .ToList();

                return Results.Ok(categorias);

            });

            group.MapGet("/{id}", async (short id, ReadMovieDb db) => {
                var categoria = await db.Categorias
                .Where(c => c.Id == id)
                .Select(c => new CategoriaDto(
                    c.Id,
                    c.Nombre
                )).FirstOrDefaultAsync();

                return Results.Ok(categoria);
            });

            group.MapPut("/{id}", async (short id, ModificarCategoriaDto dto, ReadMovieDb db) => {
                var categoria = await db.Categorias.FindAsync(id);

                if (categoria is null)
                    return Results.NotFound();

                categoria.Nombre = dto.Nombre;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
