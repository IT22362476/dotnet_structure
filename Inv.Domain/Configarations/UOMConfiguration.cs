using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Domain.Configarations
{
    public class UOMConfiguration : IEntityTypeConfiguration<UOM>
    {
        public void Configure(EntityTypeBuilder<UOM> builder)
        {
            builder.HasKey(c => c.UOMSerialID);

            builder.Property(e => e.UOMID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.UOMID");

            builder.Property(c => c.UOMID).HasColumnName("UOMID").IsRequired();

            builder.HasIndex(c => c.UOMID).IsUnique();
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
