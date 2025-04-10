using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(BrandConfiguration))]

    public class Brand : BaseAuditableEntity
    {
        [Key]
        public int BrandSerialID { get; set; }
        [Required]
        public int BrandID { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? BrandName { get; set; }
        // Navigation property for the one-to-one relationship
        public virtual ICollection<BrandItemType>? BrandItemType { get; set; }

    }
}
