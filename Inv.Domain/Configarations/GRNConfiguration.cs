using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Inv.Domain.Configarations
{

    public class GRNConfiguration : IEntityTypeConfiguration<GRN>
        {
            public void Configure(EntityTypeBuilder<GRN> builder)
            {
                builder.HasKey(c => c.GRNSerialID);

                builder.Property(e => e.GRNID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.GRNID");

                builder.Property(c => c.GRNID).HasColumnName("GRNID").IsRequired();

                builder.HasIndex(c => c.GRNID).IsUnique();
                // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
                // used for scenarios like soft deletes, multi-tenancy
                builder.HasQueryFilter(r => !r.IsDeleted);
                //Purpose: HasFilter is for defining filtered indexes at the database level
                builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
            }
        }
    
}
