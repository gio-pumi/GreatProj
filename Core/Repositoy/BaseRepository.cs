﻿using GreatProj.Core.Interfaces;
using GreatProj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GreatProj.Core.Repositoy
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _db;

        public BaseRepository(AppDbContext DbContext)
        {
            _db = DbContext;
        }
        public virtual async Task<List<T>> AddAsync(T item)
        {
            _db.Set<T>().Add(item);
            await _db.SaveChangesAsync();
            var items = await _db.Set<T>().ToListAsync();
            return items;
        }

        public virtual async Task<List<T>> DeleteAsync(long id)
        {
            T itemToDelete = await _db.Set<T>().FindAsync(id);
            _db.Set<T>().Remove(itemToDelete);
            await _db.SaveChangesAsync();
            var items = await _db.Set<T>().ToListAsync();
            return items;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _db.Set<T>().FindAsync(id);

        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _db.Set<T>().AsNoTracking();

            foreach (var property in includeProperties)
                query = query.Include(property);

            return await query.SingleOrDefaultAsync(predicate);
        }

        public virtual async Task<List<T>> UpdateAsync (T item)
        {
            _db.Set<T>().Update(item);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            var items = await _db.Set<T>().ToListAsync();
            return items;
        }
    }
}