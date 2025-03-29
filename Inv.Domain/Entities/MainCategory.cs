using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(MainCategoryConfiguration))]
    public class MainCategory : BaseAuditableEntity
    {
        [Key]
        public int MainCategorySerialID { get; set; }
        [Required]
        public int MainCategoryID { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? MainCategoryName { get; set; }
    }
}
