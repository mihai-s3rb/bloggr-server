using Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<TEntity?> GetById(int id);

        public IQueryable<TEntity> Query();

        public Task<IEnumerable<TEntity>> GetAll();

        public Task<TEntity> Add(TEntity entity);

        public Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities);

        public Task<TEntity> Remove(TEntity entity);

        public Task<TEntity?> RemoveById(int id);

        public Task<IEnumerable<TEntity>> RemoveRange(IEnumerable<TEntity> entities);

        public Task<TEntity> Update(TEntity entity);

        public Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> expression);
    }
}
