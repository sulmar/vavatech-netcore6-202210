using AuthService.Api.Domain;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Api.Infrastructure
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly IDictionary<int, User> _users;

        public InMemoryUserRepository(IPasswordHasher<User> passwordHasher)
        {
            var users = new List<User>
            {
                new User { Id = 1, UserName = "john", HashedPassword = "123", Email = "john@domain.com", DateOfBirth = DateTime.Parse("1980-01-01") },
                new User { Id = 2, UserName = "kate", HashedPassword = "123", Email = "kate@domain.com", DateOfBirth = DateTime.Parse("1990-01-01") },
                new User { Id = 3, UserName = "bob", HashedPassword = "123" , Email = "bob@domain.com", DateOfBirth = DateTime.Parse("2010-01-01")},
            };

            foreach (var user in users)
            {
                user.HashedPassword = passwordHasher.HashPassword(user, user.HashedPassword);
            }

            _users = users.ToDictionary(c => c.Id);
        }

        public User GetByUsername(string username)
        {
            return _users.Values.SingleOrDefault(u => u.UserName == username);
        }
    }
}
