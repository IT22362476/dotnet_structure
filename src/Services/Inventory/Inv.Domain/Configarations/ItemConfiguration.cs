using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Domain.Configarations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(c => c.ItemSerialID);

            builder.Property(e => e.ItemID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.ItemID");
            builder.Property(c => c.ItemID).HasColumnName("ItemID").IsRequired();

            builder.HasIndex(c => c.ItemID).IsUnique();

            builder.HasOne(u => u.Brand).WithMany().HasForeignKey(u => u.BrandSerialID);

            builder.HasOne(u => u.Model).WithMany().HasForeignKey(u => u.ModelSerialID);
            builder.HasOne(u => u.MainCategory).WithMany().HasForeignKey(u => u.MainCategorySerialID);

            builder.HasOne(u => u.SubCategory).WithMany().HasForeignKey(u => u.SubCategorySerialID);
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
