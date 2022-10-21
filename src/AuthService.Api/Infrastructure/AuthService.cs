using AuthService.Api.Domain;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace AuthService.Api.Infrastructure
{
    public class MyAuthService : IAuthService
    {
        private readonly IUserRepository userRepository;

        public MyAuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool TryAuthorize(string username, string password, out User user)
        {
            user = userRepository.GetByUsername(username);

            return user != null && user.HashedPassword == password;
        }
    }
}
