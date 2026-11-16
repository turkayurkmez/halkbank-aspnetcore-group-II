using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class User :IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public List<Role> Roles { get; set; }

    }

    public class Role :IEntity
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public int Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }


    }

    public class RefreshToken : IEntity
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string? ReplacedByToken { get; set; }
    }
}
