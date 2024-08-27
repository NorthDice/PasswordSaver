using Microsoft.AspNetCore.Mvc;
using PasswordSaver.Application;
using PasswordSaver.Models.User;
using PasswordSaver.Models;

namespace PasswordSaver.Controllers
{
    [Route("authorize")]
    public class AuthorizationController : Controller
    {
        private readonly UserService _userService;

        public AuthorizationController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
               
            await _userService.Register(request.Username, request.Password, request.Email);
            return RedirectToAction("Login");       
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            if (ModelState.IsValid)
            {
                
                    var token = await _userService.Login(request.Email, request.Password);
                    Response.Cookies.Append("tasty-cookies", token);
                    return RedirectToAction("Index", "Home");
                
                    //ModelState.AddModelError("", ex.Message);
                
            }
            return View(request);
        }
    }
}
