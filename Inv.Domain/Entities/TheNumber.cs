using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(TheNumberConfiguration))]
    public class TheNumber: BaseAuditableEntity
    {
        [Key]
        public int TheNumberSerialID { get; set; }
        [Required]
        public int TheNumberID { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The number name cannot exceed 60 characters. ")]
        public string? TheNumberName { get; set; }
        public int? ComSerialID { get; set; }
        public int LastNumber { get; set; } = 0;

    }
}


 		
