using PasswordSaver.Application;
using PasswordSaver.Models.User; 

namespace PasswordSaver.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder builder)
        {
            {
                builder.MapPost("register", Register);

                builder.MapPost("login", Login);
                return builder;
            }

        }

        public static async Task<IResult> Register(
            RegisterUserRequest request,
            UserService usersService)
        {
            await usersService.Register(request.Username, request.Email, request.Password);
             
            return Results.Ok();
        }

        public static async Task<IResult> Login(
            LoginUserRequest request,
            UserService usersService,
            HttpContext context)
        { 
            var token = await usersService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("tasty-cookies",token);

            return Results.Ok(token);
        }


    }
}
