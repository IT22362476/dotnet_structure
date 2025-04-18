using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inv.Domain.Configarations
{
    public class SystemPODetailConfiguration : IEntityTypeConfiguration<SystemPODetail>
    {
        public void Configure(EntityTypeBuilder<SystemPODetail> builder)
        {
            builder.HasKey(po => po.SystemPODetailSerialID);

            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(po => !po.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(po => po.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
