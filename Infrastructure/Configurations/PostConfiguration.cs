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
            builder.HasMany<Comment>(g => g.Comments)
                .WithOne(s => s.Post)
                .HasForeignKey(s => s.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(p => p.Comments)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.SetNull);
            //builder.HasOne(p => p.Likes)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.SetNull);
            //builder.HasOne(p => p.Interests)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
