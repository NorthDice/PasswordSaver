﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PasswordSaver.Interfaces.Auth;
using PasswordSaver.Models;
using PasswordSaver.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PasswordSaver.Models.Provider
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateJwtToken(PasswordSaver.Models.User.User user)
        {
            Claim[] claims =
            {
                new(CustomClaims.UserId, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName), // Добавить имя пользователя
                new Claim(ClaimTypes.Email, user.Email)   // Добавить email пользователя
                //new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(1)
                );


            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
