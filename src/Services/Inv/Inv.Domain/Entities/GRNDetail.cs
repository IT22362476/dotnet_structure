using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(GRNDetailConfiguration))]
    public class GRNDetail : BaseAuditableEntity
    {
        [Key]
        public int GRNDetailSerialID { get; set; }

        [Required]
        public int GRNHeaderSerialID { get; set; }

        [ForeignKey(nameof(GRNHeaderSerialID))]
        public virtual GRNHeader? GRNHeader { get; set; }

        [Required]
        public int LineNumber { get; set; }

        [Required]
        public int ItemSerialID { get; set; }

        public int SystemPOSerialID { get; set; }

        [Required]
        [StringLength(50)]
        public string? BatchNumber { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public decimal WarrentyPeriod { get; set; }

        [Required]
        public int? UOMSerialID { get; set; }

        [ForeignKey(nameof(UOMSerialID))]
        public virtual UOM? UOM { get; set; }

        [Required]
        public Condition Condition { get; set; }

        [Required]
        public decimal Qty { get; set; }

        public decimal FOCQty { get; set; }

        //The net usable quantity after accounting for quality checks, damages, etc.
        //Represents the actual quantity that will be added to stock
        public decimal BatchBalQty { get; set; }

        public int? AssetCount { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; } = string.Empty;

    }

    public enum Condition { Good = 1, Reparable = 2, Defect = 3 }
}