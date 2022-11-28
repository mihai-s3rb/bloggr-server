using Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Domain.Interfaces
{
    internal interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        void AddPost(TEntity entity);
    }
}
