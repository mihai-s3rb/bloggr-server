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
        public void Configure(EntityTypeBuilder<User> builder)
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
            
        }
    }
}
