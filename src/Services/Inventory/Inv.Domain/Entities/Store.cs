using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(StoreConfiguration))]
    public class Store : BaseAuditableEntity
    {
        [Key]
        public int StoreSerialID { get; set; }

        [Required]
        public int StoreID { get; set; }
        [Required]
        public int? ComSerialID { get; set; }

        //remove from core module and add to inventory module
        //[ForeignKey(nameof(ComSerialID))]
       // public virtual Company? Company { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        [StringLength(30, ErrorMessage = "The store name cannot exceed 30 characters. ")]
        public string? StoreName { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        [ForeignKey(nameof(WHSerialID))]
        public virtual Warehouse? Warehouse { get; set; }

        public virtual ICollection<Zone>? Zones { get; set; }
    }
}
