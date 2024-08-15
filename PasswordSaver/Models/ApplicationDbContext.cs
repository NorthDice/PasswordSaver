using Microsoft.EntityFrameworkCore;
using PasswordSaver.Configurations;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}
