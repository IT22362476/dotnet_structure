using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(ModelConfiguration))]
    public class Model : BaseAuditableEntity
    {
        [Key]
        public int ModelSerialID { get; set; }
        [Required]
        public int ModelID { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? ModelName { get; set; }
        [Required]
        public int BrandSerialID { get; set; }
        [ForeignKey(nameof(BrandSerialID))]
        public virtual Brand? Brand { get; set; }
    }
}
