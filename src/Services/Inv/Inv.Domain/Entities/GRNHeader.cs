using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(GRNHeaderConfiguration))]
    public class GRNHeader : BaseAuditableEntity
    {
        [Key]
        public int GRNHeaderSerialID { get; set; }

        [Required]
        public int GRNID { get; set; }

        [Required]
        public int CompSerialID { get; set; }

        [Required]
        public int SupplierSerialID { get; set; }

        [Required]
        [StringLength(50)]
        public string? SupplierInvoiceNumber { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        [ForeignKey(nameof(WHSerialID))]
        public virtual Warehouse? Warehouse { get; set; }

        [Required]
        public int StoreSerialID { get; set; }

        [ForeignKey(nameof(StoreSerialID))]
        public virtual Store? Store { get; set; }

        public virtual ICollection<GRNDetail> GRNDetails { get; set; } = new List<GRNDetail>();

        [Required]
        public bool Printed { get; set; }

        [Required]
        public int PreparedBy { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }
        
        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }




}
