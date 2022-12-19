using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<User> Users { get; }

        IPostRepository Posts { get; }

        IBaseRepository<Comment> Comments { get; }

        IBaseRepository<Like> Likes { get; }

        IBaseRepository<Interest> Interests { get; }

        IBaseRepository<InterestPost> InterestPosts { get; }

        public void Dispose();

        public Task Save();

    }
}
