using AuthService.Api.Domain;
using AuthService.Api.Infrastructure;
using AuthService.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthService, MyAuthService>();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();
builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

app.MapPost("api/token/create", (AuthModel model, IAuthService authService, ITokenService tokenService, HttpResponse response) =>
{
    if (authService.TryAuthorize(model.Login, model.Password, out User user))
    { 
        var token = tokenService.Create(user);

        response.Cookies.Append("X-Access-Token", token, new CookieOptions { SameSite = SameSiteMode.Strict, Secure = true });

        return Results.Ok(token);
    }
    else
    {
        return Results.BadRequest(new { message = "Username or password is incorrect." });
    }
}).WithName("CreateToken");

    app.MapGet("/", (LinkGenerator linker) => $"Use endpoint POST {linker.GetPathByName("CreateToken", null)}");

    


app.Run();
