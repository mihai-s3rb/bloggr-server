using Bloggr.Domain.Interfaces;
using Bloggr.Infrastructure.Repositories;
using Domain.Entities;
using Infrastructure.Context;

namespace Bloggr.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BloggrContext _context;

        public UnitOfWork(BloggrContext context)
        {
            _context = context;
            Users = new BaseRepository<User>(_context);
            Posts = new BaseRepository<Post>(_context);
            Comments = new BaseRepository<Comment>(_context);
            Likes = new BaseRepository<Like>(_context);
            Interests = new BaseRepository<Interest>(_context);
        }

        public IBaseRepository<User> Users { get; private set; }

        public IBaseRepository<Post> Posts { get; private set; }
        
        public IBaseRepository<Comment> Comments { get; private set; }

        public IBaseRepository<Like> Likes { get; private set; }

        public IBaseRepository<Interest> Interests { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async void Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
