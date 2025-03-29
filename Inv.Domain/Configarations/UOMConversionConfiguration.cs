using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Inv.Domain.Configarations
{
    public class UOMConversionConfiguration : IEntityTypeConfiguration<UOMConversion>
    {
        public void Configure(EntityTypeBuilder<UOMConversion> builder)
        {
            builder.HasKey(c => c.UOMConvSerialID);

            builder.Property(e => e.UOMConvID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.UOMConvID");

            builder.Property(c => c.UOMConvID).HasColumnName("UOMConvID").IsRequired();

            builder.HasIndex(c => c.UOMConvID).IsUnique();

            builder.HasOne(u => u.UOM).WithMany().HasForeignKey(u => u.UOMSerialID);

            builder.HasOne(u => u.UOMTo).WithMany().HasForeignKey(u => u.UOMToID);
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
