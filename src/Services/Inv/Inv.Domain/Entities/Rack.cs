using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(RackConfiguration))]

    public class Rack : BaseAuditableEntity
    {
        [Key]
        public int RackSerialID { get; set; }

        [Required]
        public int RackID { get; set; }

        [Required]
        [Display(Name = "Rack Name")]
        [StringLength(30, ErrorMessage = "The rack name cannot exceed 30 characters. ")]
        public string? RackName { get; set; }

        [Required]
        [Display(Name = "Rack Code")]
        [StringLength(10, ErrorMessage = "The rack code cannot exceed 10 characters. ")]
        public string? RackCode { get; set; }

        [Required]
        public int Rows { get; set; }

        [Required]
        public int Columns { get; set; }

        public int? Compartments { get; set; }

        [Required]
        public int ComSerialID { get; set; }

       //remove from core module and add to inventory module
       // [ForeignKey(nameof(ComSerialID))]
      //  public virtual Company? Company { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        [ForeignKey(nameof(WHSerialID))]
        public virtual Warehouse? Warehouse { get; set; }
        [Required]
        public int StoreSerialID { get; set; }

        [ForeignKey(nameof(StoreSerialID))]
        public virtual Store? Store { get; set; }
        [Required]
        public int ZoneSerialID { get; set; }

        [ForeignKey(nameof(ZoneSerialID))]
        public virtual Zone? Zone { get; set; }
    }
}
