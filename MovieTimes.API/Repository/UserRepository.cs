using Microsoft.EntityFrameworkCore;
using MovieTimes.API.Data;
using MovieTimes.API.DTO;
using MovieTimes.API.Models;

namespace MovieTimes.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        //get all
        public async Task<List<User>> GetUsers() =>
                await _context.Users.Select(u => new User
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Email = u.Email
                }).ToListAsync();



        //add user 
        public async Task<User> AddUser(UserAddDTO newUser)
        {
            var user = new User
            {
                Nombre = newUser.Nombre,
                Apellido = newUser.Apellido,
                Email = newUser.Email
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

    }
}
