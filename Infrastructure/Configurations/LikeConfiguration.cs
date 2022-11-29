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
        }
    }
}
