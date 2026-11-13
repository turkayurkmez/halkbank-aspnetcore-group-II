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

        //tüm repo'lar buradan kontrol edilecek

        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {
        private IProductRepository? products;
        public IProductRepository Products => products ??= new EFProductRepository(db);


        private IRepository<Category> categories;
        public IRepository<Category> Categories => categories ??= new Repository<Category>(db);

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
