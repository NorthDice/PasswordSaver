using PasswordSaver.Models.User;

namespace PasswordSaver.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}