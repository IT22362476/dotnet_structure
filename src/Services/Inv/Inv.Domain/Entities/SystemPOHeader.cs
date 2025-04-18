using System.ComponentModel.DataAnnotations;
using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;

namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(SystemPOHeaderConfiguration))]
    public class SystemPOHeader : BaseAuditableEntity
    {
        [Key]
        public int SystemPOHeaderSerialID { get; set; }

        [Required]
        public int SystemPOID { get; set; }

        [Required]
        public int CompSerialID { get; set; }

        [Required]
        public int SupplierSerialID { get; set; }

        [Required]
        public int BillingLocationSerialID { get; set; }

        [Required]
        public int PayTermSerialID { get; set; }

        [Required]
        public int IncotermSerialID { get; set; }

        [StringLength(20)]
        public string ShipMode { get; set; } = string.Empty;

        public virtual ICollection<SystemPODetail>? SystemPODetails { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; } = string.Empty;

        [Required]
        public bool IsComplete { get; set; }

        [Required]
        public int PreparedBy { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }
    }
}
