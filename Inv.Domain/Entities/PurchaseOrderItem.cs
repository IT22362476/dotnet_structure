using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(PurchaseOrderItemConfiguration))]

    public class PurchaseOrderItem : BaseAuditableEntity
    {
        [Key]
        public int POItemSerialID { get; set; }
        public int POItemID { get; set; }
        public int POSerialID { get; set; }
        public int ItemSerialID { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(POSerialID))]
        public virtual PurchaseOrder? PurchaseOrder { get; set; }
    }
}
