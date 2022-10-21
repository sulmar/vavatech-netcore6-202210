using Vavatech.Shopper.Domain;

namespace AuthService.Api.Domain
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
    }
}
