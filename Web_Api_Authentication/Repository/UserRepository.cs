using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Web_Api_Authentication.Data;
using Web_Api_Authentication.Interfaces.Repository;
using Web_Api_Authentication.Models;

namespace Web_Api_Authentication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _context;
        public UserRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async void PostUser(UserEntityModel model)
        {
            _context.Users.Add(model);
            await _context.SaveChangesAsync();

        }
    }
}