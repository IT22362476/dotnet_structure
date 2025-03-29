using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(GRNConfiguration))]
    public class GRN : BaseAuditableEntity
    {
        [Key]
        public int GRNSerialID { get; set; } // Primary Key
        [Required]
        public int GRNID { get; set; }
        [Required]
        public int SupplierSerialID { get; set; }
        [ForeignKey(nameof(SupplierSerialID))]
        public virtual Supplier? Supplier { get; set; }
        [Required]
        public DateTime ReceivedDate { get; set; }
        public string? Notes { get; set; }
        [Required]
        public string GRNNumber { get; set; } = string.Empty; // Generated from LastNumber
        public int POSerialID { get; set; }
        // Navigation Properties
        [ForeignKey(nameof(POSerialID))]
        public virtual PurchaseOrder? PurchaseOrder { get; set; }
        public ICollection<Invoice>? Invoice { get; set; }
        public ICollection<GRNLineItem>? GRNItem { get; set; }
    }

}
