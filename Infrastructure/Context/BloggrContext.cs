using Bloggr.Domain.Entities;
using Bloggr.Infrastructure.Configurations;
using Domain.Abstracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class BloggrContext : DbContext
    {
        public BloggrContext(DbContextOptions<BloggrContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new PostConfiguration().Configure(modelBuilder.Entity<Post>());
            new CommentConfiguration().Configure(modelBuilder.Entity<Comment>());
            new LikeConfiguration().Configure(modelBuilder.Entity<Like>());
            new InterestConfiguration().Configure(modelBuilder.Entity<Interest>());

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Interest> Interests { get; set; }


    }
}
