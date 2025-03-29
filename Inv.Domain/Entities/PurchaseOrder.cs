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
    [EntityTypeConfiguration(typeof(PurchaseOrderConfiguration))]
    public class PurchaseOrder : BaseAuditableEntity
    {
        [Key]
        public int POSerialID { get; set; }
        public int POID { get; set; }
        public int SupplierSerialID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(SupplierSerialID))]
        public virtual Supplier? Supplier { get; set; }
        public ICollection<GRN>? GRN { get; set; }
        public ICollection<PurchaseOrderItem>? PurchaseOrderItem { get; set; }
    }
}
