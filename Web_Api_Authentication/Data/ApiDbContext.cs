using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Web_Api_Authentication.Models;

namespace Web_Api_Authentication.Data
{
    public class ApiDbContext : DbContext
    {
       public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        public DbSet<UserEntityModel> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
               builder.Entity<UserEntityModel>().HasKey(m => m.Codigo);
               base.OnModelCreating(builder);
        }


    }
}