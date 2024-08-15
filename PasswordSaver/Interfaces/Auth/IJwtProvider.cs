using PasswordSaver.Models.User;

namespace PasswordSaver.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string GenerateJwtToken(User user);
    }
}
