using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Configurations
{
    public class PostConfiguration : BaseEntityConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(t => t.Content)
                .IsRequired()
                .HasMaxLength(10000);

            //relationships
            builder.HasMany<Comment>(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany<Like>(p => p.Likes)
                .WithOne(l => l.Post)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany<Interest>(p => p.Interests)
                .WithMany(i => i.Posts);
        }
    }
}
