using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Inv.Domain.Configarations
{
    public class RackConfiguration   : IEntityTypeConfiguration<Rack>
    {
        public void Configure(EntityTypeBuilder<Rack> builder)
        {
            builder.HasKey(c => c.RackSerialID);

            builder.Property(e => e.RackID).HasDefaultValueSql("NEXT VALUE FOR dbo.RackID");

            builder.HasIndex(c => c.RackID).IsUnique();

            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
