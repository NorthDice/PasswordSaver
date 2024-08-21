using PasswordSaver.Enums;
using PasswordSaver.Interfaces;
using System.Drawing.Text;
using System.Security;

namespace PasswordSaver.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUserRepository _userRepository;

        public PermissionService(IUserRepository usersRepository)
        {
            _userRepository = usersRepository;
        }

        public Task<HashSet<Permissions>> GetPermissionsAsync(Guid userId)
        {
            return _userRepository.GetUserPermissions(userId);
        }
    }
}
