using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext db) : Repository<User>(db), IUserRepository
    {
        public async Task<User?> FindByEmailAsync(string email)
        {
            return await db.Users.AsNoTracking().Include(u=>u.Roles).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Role?> FindRoleAsync(string name)
        {
            return await db.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
