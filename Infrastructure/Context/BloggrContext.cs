using Bloggr.Domain.Entities;
using Bloggr.Infrastructure.Configurations;
using Domain.Abstracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class BloggrContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public BloggrContext(DbContextOptions<BloggrContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(x => x.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new PostConfiguration().Configure(modelBuilder.Entity<Post>());
            new CommentConfiguration().Configure(modelBuilder.Entity<Comment>());
            new LikeConfiguration().Configure(modelBuilder.Entity<Like>());
            new InterestConfiguration().Configure(modelBuilder.Entity<Interest>());
            new InterestPostConfiguration().Configure(modelBuilder.Entity<InterestPost>());
            new InterestUserConfiguration().Configure(modelBuilder.Entity<InterestUser>());
            new RolesConfiguration().Configure(modelBuilder.Entity<IdentityRole<int>>());
            new BookmarksConfiguration().Configure(modelBuilder.Entity<Bookmark>());
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<InterestPost> InterestPosts { get; set; }

        public DbSet<Bookmark> Bookmarks { get; set; }

    }
}
