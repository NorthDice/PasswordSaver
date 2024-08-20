using PasswordSaver.Application;
using Microsoft.EntityFrameworkCore;
using PasswordSaver.Interfaces;
using PasswordSaver.Interfaces.Auth;
using PasswordSaver.Mapper;
using PasswordSaver.Models;
using PasswordSaver.Models.Provider;
using PasswordSaver.Repositories;
using PasswordSaver.Mapper;
using System.Reflection;
using Microsoft.AspNetCore.CookiePolicy;
using PasswordSaver.Extensions;
using Microsoft.Extensions.Options;
using PasswordSaver.Authentification;
using PasswordSaver.Entities;
using PasswordSaver.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(TestMapper));
//builder.Services.AddScoped<UserService>();

// Add services to the container.
var services = builder.Services;
var config = builder.Configuration;

services.AddApiAuthentication(builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
services.Configure<AuthorizationOptions>(config.GetSection(nameof(AuthorizationOptions)));
//using (var scope = builder.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    dbContext.Database.Migrate();
//}

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<UserService>();


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
}).RequireAuthorization(policy =>
    policy.AddRequirements(new PermissionRequirement([Permissions.Read])));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
