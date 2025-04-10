using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Inv.Domain.Configarations
{
    public class ZoneConfiguration  : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.HasKey(c => c.ZoneSerialID);

            builder.Property(e => e.ZoneID).HasDefaultValueSql("NEXT VALUE FOR dbo.ZoneID");

            builder.HasIndex(c => c.ZoneID).IsUnique();
            // Define the relationship to Store
            builder.HasOne(z => z.Store)
                   .WithMany(s => s.Zones)
                   .HasForeignKey(z => z.StoreSerialID)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete or update
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
