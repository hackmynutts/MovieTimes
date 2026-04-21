using Moq;
using MovieTimes.API.DTO;
using MovieTimes.API.Models;
using MovieTimes.API.Repository;
using MovieTimes.API.Services;

namespace MovieTimes.Tests
{
    public class MovieServicesTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly MovieServices _movieServices;

        public MovieServicesTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _movieServices = new MovieServices(_movieRepositoryMock.Object);
        }

        [Fact]
        public async Task GetMovies_ReturnsListOfMovieDTOs()
        {
            var fakeMovies = new List<Movie>
            {
                new Movie
                {
                    id = 1,
                    tmdb_id = 550,
                    title = "Fight Club",
                    original_title = "Fight Club",
                    overview = "An insomniac office worker...",
                    release_date = new DateOnly(1999, 10, 15),
                    poster_path = "/someImage.jpg",
                    RentedByUserId = 0
                },
                new Movie
                {
                    id = 2,
                    tmdb_id = 680,
                    title = "Pulp Fiction",
                    original_title = "Pulp Fiction",
                    overview = "The lives of two mob hitmen...",
                    release_date = new DateOnly(1994, 9, 10),
                    poster_path = "/anotherImage.jpg",
                    RentedByUserId = 1
                }
            };

            _movieRepositoryMock
                .Setup(r => r.GetMovies())
                .ReturnsAsync(fakeMovies);

            var result = await _movieServices.GetMovies();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Fight Club", result[0].title);
            Assert.Equal("Pulp Fiction", result[1].title);
        }

        [Fact]
        public async Task GetMovieById_ExistingId_ReturnsMovieDTO()
        {
            var fakeMovie = new Movie
            {
                id = 1,
                tmdb_id = 550,
                title = "Fight Club",
                original_title = "Fight Club",
                overview = "An insomniac office worker...",
                release_date = new DateOnly(1999, 10, 15),
                poster_path = "/someImage.jpg"
            };

            _movieRepositoryMock
                .Setup(r => r.GetMovieById(1))
                .ReturnsAsync(fakeMovie);

            var result = await _movieServices.GetMovieById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.id);
            Assert.Equal("Fight Club", result.title);
            Assert.Equal(550, result.tmdb_id);
        }

        [Fact]
        public async Task GetMovieById_NonExistingId_ReturnsNull()
        {
            _movieRepositoryMock
                .Setup(r => r.GetMovieById(999))
                .ReturnsAsync((Movie?)null);

            var result = await _movieServices.GetMovieById(999);

            Assert.Null(result);
        }
    }
}