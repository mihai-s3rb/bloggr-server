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
    public class InterestPostConfiguration : BaseEntityConfiguration<InterestPost>
    {
        public override void Configure(EntityTypeBuilder<InterestPost> builder)
        {
            base.Configure(builder);

            builder.Ignore(intpost => intpost.Id);
            builder.HasKey(intpost => new { intpost.PostId, intpost.InterestId });

            builder.HasOne(intpost => intpost.Post)
                 .WithMany(p => p.InterestPosts)
                 .HasForeignKey(intpost => intpost.PostId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(intpost => intpost.Interest)
                 .WithMany(i => i.InterestPosts)
                 .HasForeignKey(intpost => intpost.InterestId)
                 .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
