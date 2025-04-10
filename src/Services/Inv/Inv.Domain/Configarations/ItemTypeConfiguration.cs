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
    public class ItemTypeConfiguration : IEntityTypeConfiguration<ItemType>
    {
        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.HasKey(c => c.ItemTypeSerialID);
            builder.Property(e => e.ItemTypeSerialID).UseIdentityColumn(10, 1); // Seed = 10, Increment = 1

            builder.Property(e => e.ItemTypeID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.ItemTypeID");

            builder.Property(c => c.ItemTypeID).HasColumnName("ItemTypeID").IsRequired();

            builder.HasIndex(c => c.ItemTypeID).IsUnique();
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
