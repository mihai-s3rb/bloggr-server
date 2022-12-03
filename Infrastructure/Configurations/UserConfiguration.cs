using Bloggr.Domain.Entities;
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
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(u => u.Username)
                .IsUnique();
            builder.Property(t => t.FirstName)
                .HasMaxLength(30);
            builder.Property(t => t.LastName)
                .HasMaxLength(30);
            builder.Property(t => t.Bio)
                .HasMaxLength(1000);

            //relationships
            builder.HasMany<Post>(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany<Interest>(u => u.Interests)
                .WithMany(i => i.Users);
            builder.HasMany<Like>(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);
            builder.HasMany<Comment>(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        }
    }
}
