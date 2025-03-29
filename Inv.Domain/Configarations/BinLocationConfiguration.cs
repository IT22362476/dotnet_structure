using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Inv.Domain.Configarations
{
    public class BinLocationConfiguration : IEntityTypeConfiguration<BinLocation>
    {
        public void Configure(EntityTypeBuilder<BinLocation> builder)
        {
            builder.HasKey(c => c.BinLctnSerialID);

            builder.Property(e => e.BinLctnID).HasDefaultValueSql("NEXT VALUE FOR dbo.BinLctnID");
            
            builder.HasIndex(c => c.BinLctnID).IsUnique();

            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);

            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
