namespace Vavatech.Shopper.Domain
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
    }
}