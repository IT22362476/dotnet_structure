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
    public class MainCategoryConfiguration : IEntityTypeConfiguration<MainCategory>
    {
        public void Configure(EntityTypeBuilder<MainCategory> builder)
        {
            builder.HasKey(c => c.MainCategorySerialID);

            builder.Property(e => e.MainCategoryID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.MainCategoryID");

            builder.Property(c => c.MainCategoryID).HasColumnName("MainCategoryID").IsRequired();

            builder.HasIndex(c => c.MainCategoryID).IsUnique();
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
