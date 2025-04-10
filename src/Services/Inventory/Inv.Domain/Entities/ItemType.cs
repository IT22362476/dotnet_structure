using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(ItemTypeConfiguration))]
    public class ItemType : BaseAuditableEntity
    {
        [Key]
        public int ItemTypeSerialID { get; set; }
        [Required]
        public int ItemTypeID { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? ItemTypeName { get; set; }
        public ItemTypeCode ItemTypeCode { get; set; }
     }
    public enum ItemTypeCode
    {
        Machinery = 10,
        SpareParts = 11,
        Electrical = 12,
        Electronic = 13,
        Vehicle = 14,
        Consumer = 15,
        Stationery = 16,
        Construction = 17,
        Medicine = 18,
        Service = 19,
        Other = 20
    }
}
