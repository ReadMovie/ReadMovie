using Microsoft.EntityFrameworkCore;
using ReadMovie.Models;

namespace ReadMovie.Data
{
    public class ReadMovieDb : DbContext
    {
        public ReadMovieDb(DbContextOptions<ReadMovieDb> options) : base(options) { }

        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Genero> Generos => Set<Genero>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Pelicula> Peliculas => Set<Pelicula>();
        public DbSet<Libro> Libros => Set<Libro>();
        public DbSet<Criterio> Criterios => Set<Criterio>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(e =>
            {
                e.Property(x => x.Nombre).IsRequired();
                e.HasMany(x => x.Libros)
                .WithOne(x => x.Categoria)
                .HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
                e.HasMany(x => x.Peliculas)
                .WithOne(x => x.Categoria)
                .HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Genero>(e => 
            {
                e.Property(x => x.Nombre).IsRequired();
                e.HasMany(x => x.Libros)
                .WithOne(x => x.Genero)
                .HasForeignKey(x => x.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);
                e.HasMany(x => x.Peliculas)
                .WithOne(x => x.Genero)
                .HasForeignKey(x => x.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Usuario>(e => 
            {
                e.Property(x => x.Nombre).IsRequired();
                e.Property(x => x.FechaNacimiento).IsRequired();
                e.Property(x => x.Email).IsRequired();
                e.Property(x => x.Clave).IsRequired();
                e.Property(x => x.Rol).HasDefaultValue(0);
                e.HasMany(x => x.Criterios)
                .WithOne(x => x.Usuario)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Pelicula>(e =>
            {
                e.Property(x => x.Titulo).IsRequired();
                e.Property(x => x.Director).IsRequired();
                e.Property(x => x.FechaLanzamineto).IsRequired();
                e.Property(x => x.Resumen).IsRequired();
                e.HasMany(x => x.Criterios)
                .WithOne(x => x.Pelicula)
                .HasForeignKey(x => x.PeliculaId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Libro>(e =>
            {
                e.Property(x => x.Titulo).IsRequired();
                e.Property(x => x.Autor).IsRequired();
                e.Property(x => x.FechaPublicacion).IsRequired();
                e.Property(x => x.Resumen).IsRequired();
                e.HasMany(x => x.Criterios)
                .WithOne(x => x.Libro)
                .HasForeignKey(x => x.LibroId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Criterio>(e =>
            {
                e.Property(x => x.Puntuacion).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
