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
        }
    }
    
}
