using AuthService.Api.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Api.Infrastructure
{
    // dotnet add package System.IdentityModel.Tokens.Jwt
    public class JwtTokenService : ITokenService
    {
        public string Create(User user)
        {
            string secretKey = "your-256-bit-secret";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var identity = new ClaimsIdentity();

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            identity.AddClaim(new Claim(ClaimTypes.Role, "Developer"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Trainer"));
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "http://myauthapi.com",
                Audience = "http://myshopper.com",
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = credentials,
                Subject = identity
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
