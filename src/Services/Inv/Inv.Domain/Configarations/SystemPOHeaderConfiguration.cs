using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inv.Domain.Configarations
{
    public class SystemPOHeaderConfiguration : IEntityTypeConfiguration<SystemPOHeader>
    {
        public void Configure(EntityTypeBuilder<SystemPOHeader> builder)
        {
            builder.HasKey(po => po.SystemPOHeaderSerialID);

            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(po => !po.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(po => po.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
