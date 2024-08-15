using System.ComponentModel.DataAnnotations;

namespace PasswordSaver.Models.User
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
}
