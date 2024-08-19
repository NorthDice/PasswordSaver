using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordSaver.Entities;
using PasswordSaver.Enums;
using PasswordSaver.Repositories;

namespace PasswordSaver.Configurations
{
    public class RolePermissionConfiguration
        : IEntityTypeConfiguration<RolePermissionEntity>
    {
        private AuthorizationOptions _authorization;

        public RolePermissionConfiguration(AuthorizationOptions authorization) 
        { 
            _authorization = authorization;
        
        }
        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.HasKey(r => new { r.RoleId, r.PermissionId });

            builder.HasData(ParseRolePermissions());
        }

        private RolePermissionEntity[] ParseRolePermissions()
        {
            return _authorization.RolePermissions
                .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermissionEntity
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permissions>(p)
                }))
                .ToArray();
        }
    }
}
