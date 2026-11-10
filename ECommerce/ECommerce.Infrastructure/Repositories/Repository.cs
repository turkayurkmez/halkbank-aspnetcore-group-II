using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Infrastructure.Repositories
{
    public class Repository<T>(AppDbContext db) : IRepository<T> where T : class, IEntity, new()
    {

        protected AppDbContext Db { get; } = db;
        protected DbSet<T> Set => Db.Set<T>();
        public async Task<T> CreateAsync(T entity)
        {
            await Set.AddAsync(entity);
            return entity;
        }

        public void DeleteAsync(T entity)
        {
            Set.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            var query = Set.AsNoTracking();
            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetbyIdAsync(int id)
        {
            return await Set.FindAsync(id);
        }


        public virtual async Task<bool> IsExistsAsync(int id)
        {
            await Task.CompletedTask;
            return false;
        }


        public void UpdateAsync(T entity)
        {
            Set.Update(entity);
        }
    }
}
