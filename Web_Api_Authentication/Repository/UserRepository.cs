using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<UserEntityModel>> GetAllUsers()
        {
            var response = await _context.Users.ToListAsync();
            return response;
        }

        public async Task<IEnumerable<UserEntityModel>> GetUserByCode(long codigo)
        {
            var response = await _context.Users.Where(x => x.Codigo == codigo).ToListAsync();
            return response;
        }

        public void PostUser(UserEntityModel item, UserEntityModel model)
        {
                _context.Users.Add(model);
                _context.SaveChanges();
            
        }
    }
}