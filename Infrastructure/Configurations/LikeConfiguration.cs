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
    public class LikeConfiguration : BaseEntityConfiguration<Like>
    {
        public override void Configure(EntityTypeBuilder<Like> builder)
        {
            base.Configure(builder);

            builder.HasKey(like => new { like.PostId, like.UserId });

            builder.HasOne(like => like.Post)
                 .WithMany(p => p.Likes)
                 .HasForeignKey(like => like.PostId)
                 .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(like => like.User)
                 .WithMany(u => u.Likes)
                 .HasForeignKey(like => like.UserId)
                 .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
