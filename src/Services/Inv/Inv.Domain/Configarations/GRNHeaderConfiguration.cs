using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Inv.Domain.Configarations
{
    public class GRNHeaderConfiguration : IEntityTypeConfiguration<GRNHeader>
    {
        public void Configure(EntityTypeBuilder<GRNHeader> builder)
        {
            builder.HasKey(g => g.GRNHeaderSerialID);

            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(g => !g.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(g => g.IsDeleted).HasFilter("[IsDeleted] = 1");

            // For GRN listing query (faster joins)
            builder.HasIndex(g => g.CompSerialID);
            //builder.HasIndex(g => g.SupplierSerialID);
            builder.HasIndex(g => g.StoreSerialID);

            // For approval workflow queries
            builder.HasIndex(g => g.ApprovedBy)
                  .HasFilter("[ApprovedBy] IS NOT NULL");

            // For date-based reporting
            builder.HasIndex(g => g.CreatedDate);

            // For WHERE ApprovedBy IS NOT NULL condition
            builder.HasIndex(g => new { g.ApprovedBy, g.IsDeleted })
                  .HasFilter("[ApprovedBy] IS NOT NULL");
        }
    }
    
}
