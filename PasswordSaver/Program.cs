using PasswordSaver.Application;
using Microsoft.EntityFrameworkCore;
using PasswordSaver.Interfaces;
using PasswordSaver.Interfaces.Auth;
using PasswordSaver.Mapper;
using PasswordSaver.Models;
using PasswordSaver.Models.Provider;
using PasswordSaver.Repositories;
using PasswordSaver.Extensions;
using Microsoft.Extensions.Options;
using PasswordSaver.Authentification;
using PasswordSaver.Entities;
using PasswordSaver.Enums;
using Microsoft.AspNetCore.CookiePolicy;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var config = builder.Configuration;

// Настройка JwtOptions
services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
services.AddApiAuthentication(builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());


services.AddAutoMapper(typeof(TestMapper).Assembly);

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<UserService>();


services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

// Добавление DbContext
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Конфигурация AuthorizationOptions
services.Configure<AuthorizationOptions>(config.GetSection(nameof(AuthorizationOptions)));

services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("get", () =>
{
    return Results.Ok();
}).RequirePermissions(Permissions.Read);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
