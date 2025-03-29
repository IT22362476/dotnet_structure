using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(SupplierConfiguration))]
    public class Supplier : BaseAuditableEntity
    {
        [Key]
        public int SupplierSerialId { get; set; }
        [Required]
        public int SupplierId { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The supplier name must be between 3 and 200 characters.")]
        public string? SupplierName { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The contact person must be between 3 and 200 characters.")]
        public string? ContactPerson { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The phone number must be between 3 and 50 characters.")]
        public string? Phone { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The eamil must be between 3 and 100 characters.")]
        public string? Email { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "The adderss must be between 3 and 200 characters.")]
        public string? Address { get; set; }
        // Navigation Property
        public ICollection<Invoice>? Invoices { get; set; }
    }
}

