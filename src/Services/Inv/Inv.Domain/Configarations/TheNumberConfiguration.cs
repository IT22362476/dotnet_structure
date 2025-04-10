using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Domain.Configarations
{
    public class TheNumberConfiguration : IEntityTypeConfiguration<TheNumber>
    {
        public void Configure(EntityTypeBuilder<TheNumber> builder)
        {
            builder.HasKey(c => c.TheNumberSerialID);
            builder.Property(e => e.TheNumberID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.TheNumberID");
 
        }
    }
    
}
