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
    public class InterestConfiguration : BaseEntityConfiguration<Interest>
    {
        public override void Configure(EntityTypeBuilder<Interest> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);
            
        }
    }
}
