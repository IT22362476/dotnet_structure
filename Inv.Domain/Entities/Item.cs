using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(ItemConfiguration))]
    public class Item : BaseAuditableEntity
    {
        [Key]
        public int ItemSerialID { get; set; }
        [Required]
        public int ItemID { get; set; }
        [Required]
        [StringLength(30)]
        public string? ItemCode { get; set; }
        [Required]
        public int? ItemTypeSerialID { get; set; }
        [NotMapped]
        public string? ItemDes { get; set; }
        [Required]
        public int? MainCategorySerialID { get; set; }
        [Required]
        public int? SubCategorySerialID { get; set; }
        [ForeignKey(nameof(SubCategorySerialID))]

        public virtual SubCategory? SubCategory { get; set; }
        [ForeignKey(nameof(MainCategorySerialID))]

        public virtual MainCategory? MainCategory { get; set; }
        [Required]
        public int? ModelSerialID { get; set; }
        [ForeignKey(nameof(ModelSerialID))]
        public virtual Model? Model { get; set; }
        [Required]
        public int? BrandSerialID { get; set; }

        [ForeignKey(nameof(BrandSerialID))]
        public virtual Brand? Brand { get; set; }
        [ForeignKey(nameof(ItemTypeSerialID))]
        public virtual ItemType? ItemType { get; set; }

        public double Weight { get; set; }
        public double Volume { get; set; }
        [StringLength(20)]
        public string? Size { get; set; }
        [StringLength(20)]
        public string? Color { get; set; }
        [StringLength(50)]
        public string? ItemPartNo { get; set; }
        [StringLength(50)]
        public string? Article { get; set; }
        [StringLength(100)]
        public string? Remarks { get; set; }
        public double Length { get; set; } 
        public double Width { get; set; } 
        public double Height { get; set; }
        [StringLength(20)]
        public string? Guage { get; set; }
        [StringLength(100)]
        public string? Construction { get; set; }
        [StringLength(100)]
        public string? SpecialFeatures { get; set; }
        [Required]
        public int? UOMSerialID { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

    }
}
