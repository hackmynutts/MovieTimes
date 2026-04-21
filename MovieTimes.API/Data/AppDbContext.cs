using Microsoft.EntityFrameworkCore;
using MovieTimes.API.Models;

namespace MovieTimes.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuración adicional del modelo si es necesario

            modelBuilder.Entity<Movie>( entity =>
            {
                entity.Property(e => e.title).IsRequired(); // Configura 'title' como requerido
                entity.Property(e => e.original_title).IsRequired(); // Configura 'original_title' como requerido
                entity.Property(e => e.overview).IsRequired(); // Configura 'overview' como requerido
                entity.Property(e => e.release_date).IsRequired(); // Configura 'release_date' como requerido
                entity.Property(e => e.poster_path).IsRequired(); // Configura 'poster_path' como requerido
                entity.Property(e => e.tmdb_id).IsRequired(); // Configura 'tmdb_id' como requerido
                entity.Property(e => e.PosterUrl).IsRequired(); // Configura 'PosterUrl' como requerido
            });
        }
    }
}
