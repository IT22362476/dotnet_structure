using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(InvoiceItemConfiguration))]
    public class InvoiceItem : BaseAuditableEntity
    {
        [Key]
        public int InvoiceItemSerialID { get; set; }
        public int InvoiceItemID { get; set; }
        public int InvoiceSerialID { get; set; }
        public int GRNLineItemSerialID { get; set; }
        public int ItemSerialID { get; set; }
        public decimal BilledQty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal BilledAmount { get; set; }
        // Navigation Properties
        public Invoice? Invoice { get; set; }
        public GRNLineItem? GRNLineItem { get; set; }
    }
}
