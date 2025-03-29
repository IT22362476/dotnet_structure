using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(UOMConfiguration))]
    public class UOM : BaseAuditableEntity
    {
        [Key]
        public int UOMSerialID { get; set; }
        [Required]
        public int UOMID { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "The field must be between 1 and 10 characters.")]
        public string? UOMName { get; set; }
        [Required]
        public string? UOMDescription { get; set;}
    }
}
