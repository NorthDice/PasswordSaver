using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PasswordSaver.Configurations;
using PasswordSaver.Entities;
using PasswordSaver.Repositories;
using System.Net;

namespace PasswordSaver.Models
{
    public class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<AuthorizationOptions> authOptions) : DbContext (options)
    {
        
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfigurations());

            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));
        }
    }
}
