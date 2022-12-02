using Bloggr.Domain.Interfaces;
using Domain.Abstracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace Bloggr.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private BloggrContext _ctx;
        private DbSet<TEntity> _dbSet;

        public BaseRepository(BloggrContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<TEntity>();
        }

        public async Task<TEntity?> GetById(int id)
        {
            TEntity? result = await _dbSet.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            IEnumerable<TEntity> results = await _dbSet.ToListAsync();
            return results;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            _dbSet.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            await _ctx.SaveChangesAsync();
            return entities;
        }

        public async Task<TEntity?> RemoveById(int id)
        {
            TEntity? existing = await _dbSet.FindAsync(id);
            if(existing is not null)
            {
                _ctx.Remove(existing);
            }
            await _ctx.SaveChangesAsync();
            return existing;
        }

        public async Task<IEnumerable<TEntity>> RemoveRange(IEnumerable<TEntity> entities)
        {
            _ctx.RemoveRange(entities);
            await _ctx.SaveChangesAsync();
            return entities;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> expression)
        {
            IEnumerable<TEntity> results = await _dbSet.Where(expression).ToListAsync();
            return results;
        }
    }
}
