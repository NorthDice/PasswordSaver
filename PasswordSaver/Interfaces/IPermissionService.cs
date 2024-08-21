using PasswordSaver.Enums;

namespace PasswordSaver.Interfaces
{
    public interface IPermissionService
    {
        Task<HashSet<Permissions>> GetPermissionsAsync(Guid userId);
    }
}
