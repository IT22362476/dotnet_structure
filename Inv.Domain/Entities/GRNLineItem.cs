using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(GRNLineItemConfiguration))]
    public class GRNLineItem : BaseAuditableEntity
    {
        [Key]
        public int GRNLineItemSerialID { get; set; } // Primary Key
        [Required]
        public int GRNLineItemID { get; set; }
        [Required]
        public int GRNSerialID { get; set; }
        [ForeignKey(nameof(GRNSerialID))]
        public virtual GRN? GRN { get; set; }
        public int POItemSerialID { get; set; }
        public decimal ReceivedQty { get; set; }
        [ForeignKey(nameof(POItemSerialID))]
        public virtual PurchaseOrderItem? POItem { get; set; }
    }

   
}
