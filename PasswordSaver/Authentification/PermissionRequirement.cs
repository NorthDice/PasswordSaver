using Microsoft.AspNetCore.Authorization;
using PasswordSaver.Enums;

namespace PasswordSaver.Authentification
{
    public class PermissionRequirement(Permissions[] permissions) 
        : IAuthorizationRequirement
    {
        public Permissions[] Permissions { get; set; } = permissions;

    }
}
