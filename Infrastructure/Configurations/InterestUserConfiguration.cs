using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggr.Domain.Entities;

namespace Bloggr.Infrastructure.Configurations
{
    public class InterestUserConfiguration : BaseEntityConfiguration<InterestUser>
    {
        public override void Configure(EntityTypeBuilder<InterestUser> builder)
        {
            base.Configure(builder);

            builder.Ignore(intuser => intuser.Id);
            builder.HasKey(intuser => new { intuser.UserId, intuser.InterestId });

            builder.HasOne(intuser => intuser.User)
                 .WithMany(p => p.InterestUsers)
                 .HasForeignKey(intuser => intuser.UserId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(intuser => intuser.Interest)
                 .WithMany(i => i.InterestUsers)
                 .HasForeignKey(intuser => intuser.InterestId)
                 .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
