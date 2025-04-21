using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Inv.Domain.Configarations
{
    public class DelRecordConfiguration : IEntityTypeConfiguration<DelRecord>
    {
        public void Configure(EntityTypeBuilder<DelRecord> builder)
        {
            //builder.HasKey(c => c.DelRecSerialID);

            //builder.Property(e => e.DelRecID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.DelRecID");

            //builder.Property(c => c.DelRecID).HasColumnName("DelRecID").IsRequired();

            //builder.HasIndex(c => c.DelRecID).IsUnique();
            //// HasQueryFilter is used to define a global filter on a DbSet in Entity Framework EAM
            //// used for scenarios like soft deletes, multi-tenancy
            //builder.HasQueryFilter(r => !r.IsDeleted);
            ////Purpose: HasFilter is for defining filtered indexes at the database level
            //builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");

            builder.HasKey(c => c.DelRecSerialID);

            builder.Property(e => e.DelRecID)
                   .HasDefaultValueSql("NEXT VALUE FOR dbo.DelRecID")
                   .IsRequired();

            builder.HasIndex(c => c.DelRecID).IsUnique();

            builder.HasQueryFilter(r => !r.IsDeleted);

            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
