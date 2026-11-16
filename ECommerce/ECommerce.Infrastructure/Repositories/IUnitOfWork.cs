using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IRepository<Category> Categories {  get; }
        IUserRepository User { get; }
        IRepository<Role> Roles { get; }
        IRepository<RefreshToken> RefreshTokens { get; }

        //tüm repo'lar buradan kontrol edilecek

        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {
        private IProductRepository? products;
        public IProductRepository Products => products ??= new EFProductRepository(db);




        private IRepository<Category> categories;
        public IRepository<Category> Categories => categories ??= new Repository<Category>(db);


        private IUserRepository userRepository;
        public IUserRepository User => userRepository ??= new UserRepository(db);

        private IRepository<Role> roles;
        public IRepository<Role> Roles => roles ??= new Repository<Role>(db);


        private IRepository<RefreshToken> refreshTokens;
        public IRepository<RefreshToken> RefreshTokens => refreshTokens ??= new Repository<RefreshToken>(db);

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await db.SaveChangesAsync();
        }
    }
}
