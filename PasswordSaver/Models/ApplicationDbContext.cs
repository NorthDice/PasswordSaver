using Microsoft.EntityFrameworkCore;
using PasswordSaver.Entities;

namespace PasswordSaver.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<UserEntity> Users { get; set; }

    }
}
