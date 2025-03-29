using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(WarehouseConfiguration))]

    public class Warehouse : BaseAuditableEntity
    {
        [Key]
        public int WHSerialID { get; set; }

        [Required]
        public int WHID { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "The name cannot exceed 60 characters. ")]
        public string? WHName { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address1 { get; set; }

        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address2 { get; set; }

        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address3 { get; set; }

        [Required]
        public int ComSerialID { get; set; }

        //remove from core module and add to inventory module
        //[ForeignKey(nameof(ComSerialID))]
        //public virtual Company? Company { get; set; }

        public virtual ICollection<Store>? Stores { get; set; }
    }
}
