using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByEmailAsync(string email);
        Task<Role?> FindRoleAsync(string name);


    }

    public interface IRefreshTokenRepository: IRepository<RefreshToken> 
    {
        Task<RefreshToken?> FindRefreshTokenAsync(string tokenHash);
        Task<bool> TryRevokeAsync(string tokenHash, string replacedByHash);
            
        
    }


}
