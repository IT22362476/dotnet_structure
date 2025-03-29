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
    [EntityTypeConfiguration(typeof(InvoiceConfiguration))]
    public class Invoice : BaseAuditableEntity
    {
        [Key]
        public int InvoiceSerialID { get; set; } // Primary Key
        [Required]
        public int InvoiceID { get; set; }
        [Required]
        public int SupplierSerialID { get; set; }
        [ForeignKey(nameof(SupplierSerialID))]
        public virtual Supplier? Supplier { get; set; }
        [Required]
        public int GRNSerialID { get; set; }
        [ForeignKey(nameof(GRNSerialID))]
        public virtual GRN? GRN { get; set; }
       [Required]
        public DateTime InvoiceDate { get; set; }
        [Required]
        public decimal InvoiceTotal { get; set; } //            InvoiceTotal
        [Required]
        public string InvoiceNumber { get; set; } = string.Empty; // Generated from LastNumber
        public int LastNumber { get; set; } // Implements IHasLastNumber
        public string Status { get; set; } = "Pending";

        // Navigation Properties
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
