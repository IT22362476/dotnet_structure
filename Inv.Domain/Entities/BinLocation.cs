using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(BinLocationConfiguration))]

    public class BinLocation:BaseAuditableEntity
    {
        [Key]
        public int BinLctnSerialID { get; set; }
        [Required]
        public string? BinName { get; set; }
        [Required]
        public string? BinLctn { get; set; }
        [Required]
        public int BinLctnID { get; set; }

        [Required]
        public int ItemSerialID { get; set; }
        [Required]
        public int Row { get; set; }

        [Required]
        public string? Column { get; set; }

        public int? Compartment { get; set; }

        [Required]
        public int? ComSerialID { get; set; }
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
        [Required]
        public int ZoneSerialID { get; set; }

        [ForeignKey(nameof(ZoneSerialID))]
        public virtual Zone? Zone { get; set; }
        [Required]
        public int RackSerialID { get; set; }

        [ForeignKey(nameof(RackSerialID))]
        public virtual Rack? Rack { get; set; }
        [Required]
        public ItemCondition ItemCondition { get; set; }
        [Required]
        public bool IsVoidBinLocation { get; set; }=false;
    }
    public enum ItemCondition { Working=1, Reparable=2, Defect=3, Damaged=4, Lost=5, Broken=6 }
}

