using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(UOMConversionConfiguration))]
    public class UOMConversion : BaseAuditableEntity
    {
        [Key]
        public int UOMConvSerialID { get; set; }
        [Required]
        public int UOMConvID { get; set; }
        [Required]
        public int UOMSerialID { get; set; }
        [ForeignKey(nameof(UOMSerialID))]
        public virtual UOM? UOM { get; set; }
        [Required]
        public int UOMToID { get; set; }
        [ForeignKey(nameof(UOMToID))]
        public virtual UOM? UOMTo { get; set; }
        [Required]
        public decimal ConversionRate { get; set; }
        [Required]
        public string? ConversionDescription { get; set; }
    }
}
