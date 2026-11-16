using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Auth
{
    public interface IPasswordHasher
    {
        string Hash(User user, string password);
        bool VerifyPassword(User user, string password);
    }

    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<User> _inner = new();
        public string Hash(User user, string password)
        {
            return _inner.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string password)
        {
            return _inner.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Failed;
        }
    }
}
