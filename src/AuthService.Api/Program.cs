using AuthService.Api.Domain;
using AuthService.Api.Infrastructure;
using AuthService.Api.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthService, MyAuthService>();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();


var app = builder.Build();

app.MapPost("api/token/create", (AuthModel model, IAuthService authService, ITokenService tokenService, HttpResponse response) =>
{
    if (authService.TryAuthorize(model.Login, model.Password, out User user))
    { 
        var token = tokenService.Create(user);

        response.Cookies.Append("Authorization", token);

        return Results.Ok(token);
    }
    else
    {
        return Results.BadRequest(new { message = "Username or password is incorrect." });
    }
});


app.Run();
