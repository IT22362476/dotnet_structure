using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(BrandItemTypeConfiguration))]

    public class BrandItemType : BaseAuditableEntity
    {
        [Key]
        public int BITSerialID { get; set; }
        [Required]
        public int BITID { get; set; }
        [Required]
        public int BrandSerialID { get; set; }
        [Required]
        public int ItemTypeSerialID { get; set; }
        [ForeignKey(nameof(BrandSerialID))]
        public virtual Brand? Brand { get; set; }

        [ForeignKey(nameof(ItemTypeSerialID))]
        public virtual ItemType? ItemType { get; set; }
    }
}
