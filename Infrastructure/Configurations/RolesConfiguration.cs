using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Bloggr.Infrastructure.Configurations
{
    internal class RolesConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
    {
        public virtual void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
        {
            builder.HasData(new IdentityRole<int>
            {
                Id = 1,
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole<int>
            {
                Id = 2,
                Name = "Admin",
                NormalizedName = "ADMIN"
            }
            );
        }
    }
}
