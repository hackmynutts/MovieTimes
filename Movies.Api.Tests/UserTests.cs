using Moq;
using MovieTimes.API.DTO;
using MovieTimes.API.Models;
using MovieTimes.API.Repository;
using MovieTimes.API.Services;

namespace MovieTimes.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsListOfUsers()
        {
            var fakeUsers = new List<User>
            {
                new User { Id = 1, Nombre = "Carlos", Apellido = "Mora", Email = "carlos@email.com" },
                new User { Id = 2, Nombre = "Ana", Apellido = "Pérez", Email = "ana@email.com" }
            };

            _userRepositoryMock
                .Setup(r => r.GetUsers())
                .ReturnsAsync(fakeUsers);

            var result = await _userService.GetUsers();

 
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Carlos", result[0].Nombre);
            Assert.Equal("Ana", result[1].Nombre);
        }


        [Fact]
        public async Task AddUser_ValidDTO_ReturnsCreatedUser()
        {
            // Arrange
            var addDTO = new UserAddDTO
            {
                Nombre = "Luis",
                Apellido = "Solano",
                Email = "luis@email.com"
            };

            var savedUser = new User
            {
                Id = 3,
                Nombre = "Luis",
                Apellido = "Solano",
                Email = "luis@email.com"
            };

            _userRepositoryMock
                .Setup(r => r.AddUser(It.IsAny<UserAddDTO>()))
                .ReturnsAsync(savedUser);

            var result = await _userService.AddUser(addDTO);

            Assert.NotNull(result);
            Assert.Equal(3, result.Id);
            Assert.Equal("Luis", result.Nombre);
            Assert.Equal("luis@email.com", result.Email);
        }

        [Fact]
        public async Task GetUsers_EmptyRepository_ReturnsEmptyList()
        {
            // Arrange
            _userRepositoryMock
                .Setup(r => r.GetUsers())
                .ReturnsAsync(new List<User>());

            // Act
            var result = await _userService.GetUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}