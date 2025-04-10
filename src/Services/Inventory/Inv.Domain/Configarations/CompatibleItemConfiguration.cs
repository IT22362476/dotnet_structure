using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Domain.Configarations
{
    public class CompatibleItemConfiguration : IEntityTypeConfiguration<CompatibleItem>
    {
        public void Configure(EntityTypeBuilder<CompatibleItem> builder)
        {
 
            builder.HasKey(c => c.CompatibleItemSerialID);

            builder.Property(e => e.CompatibleItemlID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.CompatibleItemlID");

            builder.Property(c => c.CompatibleItemlID).HasColumnName("CompatibleItemlID").IsRequired();

            builder.HasIndex(c => c.CompatibleItemlID).IsUnique();
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
 
}
