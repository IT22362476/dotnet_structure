using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace Inv.Domain.Configarations
{
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(c => c.InvoiceSerialID);

            builder.Property(e => e.InvoiceItemID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.InvoiceItemID");

            builder.Property(c => c.InvoiceItemID).HasColumnName("InvoiceItemID").IsRequired();

            builder.HasOne(ii => ii.Invoice).WithMany(i => i.InvoiceItems).HasForeignKey(ii => ii.InvoiceSerialID);
            builder.HasOne(ii => ii.GRNLineItem).WithMany().HasForeignKey(ii => ii.GRNLineItemSerialID);
            builder.HasIndex(c => c.InvoiceItemSerialID).IsUnique();
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
