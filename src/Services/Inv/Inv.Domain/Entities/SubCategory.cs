using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(SubCategoryConfiguration))]
    public class SubCategory : BaseAuditableEntity
    {
        [Key]
        public int SubCategorySerialID { get; set; }
        [Required]
        public int SubCategoryID { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? SubCategoryName { get; set; }
        public int MainCategorySerialID { get; set; }
        [ForeignKey(nameof(MainCategorySerialID))]
        public virtual MainCategory? MainCategory { get; set; }
    }
}
