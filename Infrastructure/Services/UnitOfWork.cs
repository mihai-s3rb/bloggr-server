using Bloggr.Domain.Entities;
using Bloggr.Infrastructure.Interfaces;
using Bloggr.Infrastructure.Repositories;
using Domain.Entities;
using Infrastructure.Context;

namespace Bloggr.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BloggrContext _context;

        public UnitOfWork(BloggrContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Posts = new PostRepository(_context);
            Comments = new BaseRepository<Comment>(_context);
            Likes = new BaseRepository<Like>(_context);
            Interests = new BaseRepository<Interest>(_context);
            InterestPosts = new BaseRepository<InterestPost>(_context);
            InterestUsers = new BaseRepository<InterestUser>(_context);
            Bookmarks = new BaseRepository<Bookmark>(_context);
        }

        public IUserRepository Users { get; private set; }

        public IPostRepository Posts { get; private set; }

        public IBaseRepository<Comment> Comments { get; private set; }

        public IBaseRepository<Like> Likes { get; private set; }

        public IBaseRepository<Interest> Interests { get; private set; }

        public IBaseRepository<InterestPost> InterestPosts { get; private set; }

        public IBaseRepository<InterestUser> InterestUsers { get; private set; }

        public IBaseRepository<Bookmark> Bookmarks { get; private set; }


        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
