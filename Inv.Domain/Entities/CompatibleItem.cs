using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Domain.Entities
{
   
    [EntityTypeConfiguration(typeof(CompatibleItemConfiguration))]

    public class CompatibleItem : BaseAuditableEntity
    {
        [Key]
        public int CompatibleItemSerialID { get; set; } // Primary key for the CompatibleItem

        [Required]
        public int CompatibleItemlID { get; set; } // ID of the compatible item (foreign key)
        
        [Required]
        public int ItemSerialID { get; set; } // ID of the item (foreign key)

        [Required]
        public int ItemCompatibleSerialID { get; set; } // Another ID for compatibility (foreign key)

        // Foreign key to the "Item" entity representing the compatible item
        [ForeignKey(nameof(ItemCompatibleSerialID))]
        public Item? ItemCompatible { get; set; }

        // Foreign key to the "Item" entity representing the item being checked for compatibility
        [ForeignKey(nameof(ItemSerialID))]
        public Item? Item { get; set; }
    }
}
