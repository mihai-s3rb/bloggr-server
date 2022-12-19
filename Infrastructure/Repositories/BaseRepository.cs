using Bloggr.Domain.Exceptions;
using Bloggr.Infrastructure.Interfaces;
using Domain.Abstracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Http.Results;

namespace Bloggr.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private BloggrContext _ctx;
        protected DbSet<TEntity> _dbSet;

        public BaseRepository(BloggrContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int id)
        {
            TEntity? result = await _dbSet.FindAsync(id);
            if(result == null)
            {
                throw EntityNotFoundException.OfType<TEntity>();
            }
            return result;
        }
        public IQueryable<TEntity> Query()
        { 
            return _dbSet;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            IEnumerable<TEntity> results = await _dbSet.AsNoTracking().ToListAsync();
            return results;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return entities;
        }

        public async Task<TEntity> Remove(TEntity entity)
        {
            _ctx.Remove(entity);
            return entity;
        }

        public async Task<TEntity?> RemoveById(int id)
        {
            TEntity? existing = await _dbSet.FindAsync(id);
            if (existing == null)
            {
                throw EntityNotFoundException.OfType<TEntity>();
            }
            _ctx.Remove(existing);
            return existing;
        }

        public async Task<IEnumerable<TEntity>> RemoveRange(IEnumerable<TEntity> entities)
        {
            _ctx.RemoveRange(entities);
            return entities;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            TEntity? existing = await _dbSet.FindAsync(entity.Id);
            if (existing == null)
            {
                throw EntityNotFoundException.OfType<TEntity>();
            }
            _dbSet.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> expression)
        {
            IEnumerable<TEntity> results = await _dbSet.Where(expression).ToListAsync();
            return results;
        }
    }
}
