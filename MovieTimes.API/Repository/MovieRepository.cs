using Microsoft.EntityFrameworkCore;
using MovieTimes.API.Data;
using MovieTimes.API.Models;

namespace MovieTimes.API.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        //listar
        public async Task<List<Movie>> GetMovies()
        {
            return await _context.Movies.AsNoTracking().ToListAsync();
        }

        //obtener por id
        public async Task<Movie?> GetMovieById(int id)
        {
            return await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.id == id);
        }

        //agregar
        public async Task<Movie> AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        //actualizar
        public async Task<Movie?> UpdateMovieAsync(Movie movie)
        {
            var existingMovie = await _context.Movies.FindAsync(movie.id);
            if (existingMovie == null)
            {
                return null;
            }
            _context.Movies.Update(movie); 
            await _context.SaveChangesAsync();
            return movie;
        }

        //eliminar
        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return false;
            }
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
