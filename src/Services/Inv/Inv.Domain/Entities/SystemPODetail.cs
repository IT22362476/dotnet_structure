using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;

namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(SystemPODetailConfiguration))]
    public class SystemPODetail : BaseAuditableEntity
    {
        [Key]
        public int SystemPODetailSerialID { get; set; }

        [Required]
        public int SystemPOHeaderSerialID { get; set; }

        [ForeignKey(nameof(SystemPOHeaderSerialID))]
        public SystemPOHeader? SystemPOHeader {  get; set; }

        [Required]
        public int LineNumber { get; set; }

        [Required]
        public int ItemSerialID { get; set; }

        [Required]
        public int? UOMSerialID { get; set; }

        [Required]
        public decimal Qty { get; set; }

        [Required]
        public decimal BalToReceive { get; set; }

        [StringLength(100)]
        public string Notes { get; set; } = string.Empty;
    }
}
