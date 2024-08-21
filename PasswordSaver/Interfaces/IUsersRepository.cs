using PasswordSaver.Enums;
using PasswordSaver.Models.User;

namespace PasswordSaver.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task<HashSet<Permissions>> GetUserPermissions(Guid userId);
        Task<User> GetById(Guid userId);
    }
}