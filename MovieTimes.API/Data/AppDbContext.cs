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
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>( entity =>
            {
                entity.Property(e => e.title).IsRequired(); 
                entity.Property(e => e.original_title).IsRequired(); 
                entity.Property(e => e.overview).IsRequired();
                entity.Property(e => e.release_date).IsRequired(); 
                entity.Property(e => e.poster_path).IsRequired(); 
                entity.Property(e => e.tmdb_id).IsRequired(); 
                entity.Property(e => e.PosterUrl).IsRequired(); 

            });
        }
    }
}
