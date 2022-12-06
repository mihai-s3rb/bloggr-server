using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<User> Users { get; }

        IBaseRepository<Post> Posts { get; }

        IBaseRepository<Comment> Comments { get; }

        IBaseRepository<Like> Likes { get; }

        IBaseRepository<Interest> Interests { get; }

        public void Dispose();

        public void Save();

    }
}
