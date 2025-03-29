using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Inv.Domain.Configarations
{
    public class StoreConfiguration   : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(c => c.StoreSerialID);

            builder.Property(e => e.StoreID).HasDefaultValueSql("NEXT VALUE FOR dbo.StoreID");

            builder.HasIndex(c => c.StoreID).IsUnique();
            builder.HasOne(s => s.Warehouse)
                   .WithMany(w => w.Stores)
                   .HasForeignKey(s => s.WHSerialID)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascading
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
