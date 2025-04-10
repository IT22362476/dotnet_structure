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
     public class CusPriceCategoryConfiguration : IEntityTypeConfiguration<CusPriceCategory>
    {
        public void Configure(EntityTypeBuilder<CusPriceCategory> builder)
        {
            builder.HasKey(c => c.CusPriceCatSerialID);

            builder.Property(e => e.CusPriceCatID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.CusPriceCatID");

            builder.Property(c => c.CusPriceCatID).HasColumnName("CusPriceCatID").IsRequired();

            builder.HasIndex(c => c.CusPriceCatID).IsUnique();
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
