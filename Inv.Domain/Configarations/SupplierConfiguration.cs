using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Inv.Domain.Configarations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(c => c.SupplierSerialId);

            builder.Property(e => e.SupplierId).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.SupplierId");

            builder.Property(c => c.SupplierId).HasColumnName("SupplierId").IsRequired();

            builder.HasIndex(c => c.SupplierId).IsUnique();
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
