using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(ZoneConfiguration))]
    public class Zone : BaseAuditableEntity
    {
        [Key]
        public int ZoneSerialID { get; set; }

        [Required]
        public int ZoneID { get; set; }

        [Required]
        [Display(Name = "Zone Name")]
        [StringLength(30, ErrorMessage = "The zone name cannot exceed 30 characters. ")]
        public string? ZoneName { get; set; }

        [Required]
        public int ComSerialID { get; set; }
        //remove from core module and add to inventory module
        //[ForeignKey(nameof(ComSerialID))]
        //public virtual Company? Company { get; set; }
        [Required]
        public int WHSerialID { get; set; }

        [ForeignKey(nameof(WHSerialID))]
        public virtual Warehouse? Warehouse { get; set; }
        [Required]
        public int StoreSerialID { get; set; }

        [ForeignKey(nameof(StoreSerialID))]
        public virtual Store? Store { get; set; }
    }
}
