using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordSaver.Interfaces;
using PasswordSaver.Models.User;
using System.Security.Claims;

namespace PasswordSaver.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Profile()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userIdClaim == null || !Guid.TryParse(userIdClaim,out var userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userRepository.GetById(userId);

            var model = new User
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email
            };


            return View(model);
        }

        public IActionResult SavedPasswords()
        {

        }
    }
}
