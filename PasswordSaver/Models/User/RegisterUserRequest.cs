using System.ComponentModel.DataAnnotations;

namespace PasswordSaver.Models.User
{
    public record RegisterUserRequest(
        [Required] string Username,
        [Required] string Password,
        [Required] string Email);
}
