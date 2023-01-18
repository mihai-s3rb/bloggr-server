using Bloggr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Configurations
{
    public class BookmarksConfiguration : BaseEntityConfiguration<Bookmark>
    {
        public override void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            base.Configure(builder);

            builder.HasKey(bookmark => new { bookmark.PostId, bookmark.UserId });

            builder.HasOne(bookmark => bookmark.Post)
                 .WithMany(p => p.Bookmarks)
                 .HasForeignKey(bookmark => bookmark.PostId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bookmark => bookmark.User)
                 .WithMany(u => u.Bookmarks)
                 .HasForeignKey(bookmark => bookmark.UserId)
                 .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
