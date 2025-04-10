using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Domain.Entities
{
   
    [EntityTypeConfiguration(typeof(CusPriceCategoryConfiguration))]

    public class CusPriceCategory : BaseAuditableEntity
    {
        [Key]
        public int CusPriceCatSerialID { get; set; }
        [Required]
        public int CusPriceCatID { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? CusPriceCatName { get; set; }
      
    }
}
