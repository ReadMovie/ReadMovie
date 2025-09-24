using ReadMovie.Models;
using System.ComponentModel.DataAnnotations;

namespace ReadMovie.Dto
{
    public record LibroDto
    (
     long Id,
     short CategoriaId,
     short GeneroId,
     string Titulo,
     string Autor,
     DateTime FechaPublicacion,
     string Resumen
    );
}
