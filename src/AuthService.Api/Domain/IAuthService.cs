namespace AuthService.Api.Domain
{
    public interface IAuthService
    {
        bool TryAuthorize(string username, string password, out User user);
    }


    public interface ITokenService
    {
        string Create(User user);
    }


    public interface IUserRepository
    {
        User GetByUsername(string username);
    }
}
