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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private string profileImageUrl = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";
        private string backgroundImageUrl = @"https://img.freepik.com/free-photo/abstract-smooth-empty-grey-studio-well-use-as-background-business-report-digital-website-template-backdrop_1258-52620.jpg?w=2000";

        public virtual void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(t => t.CreationDate)
                .HasDefaultValueSql("getdate()");
            builder.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(u => u.UserName)
                .IsUnique();
            builder.Property(t => t.FirstName)
                .HasMaxLength(30);
            builder.Property(t => t.LastName)
                .HasMaxLength(30);
            builder.Property(t => t.Bio)
                .HasMaxLength(1000);
            builder.Property(t => t.ProfileImageUrl)
                .HasDefaultValue(profileImageUrl);
            builder.Property(t => t.BackgroundImageUrl)
                .HasDefaultValue(backgroundImageUrl);

            //relationships
            builder.HasMany<Post>(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany<Interest>(u => u.CreatedInterests)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany<Like>(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany<Comment>(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
