using Microsoft.EntityFrameworkCore;
using Web_Api_Authentication.Models;

namespace Web_Api_Authentication.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        public DbSet<UserEntityModel> Users { get; set; }
    }
}