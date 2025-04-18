
using Inv.Domain.Common;
using Inv.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Inv.Domain.Entities
{
    [EntityTypeConfiguration(typeof(DelRecordConfiguration))]

    public class DelRecord : BaseAuditableEntity
    {
        [Key]
        public int DelRecSerialID { get; set; }
        [Required]
        public int DelRecID { get; set; }   
        [Required]
        public string? DocTable { get; set; }
        [Required]
        public int? DocSerialID { get; set; }
        [Required]
        public string? Remarks { get; set; }
    }
}

/*DelRecords Table																	
DelRec ID																							
DocTable						eg. SupplierInvoice, GRN, Internsl, IntAssetReg, RentAssetReg																	
DocID						Serial ID of the record in DocType table																	
Remarks																							
Del By																							
Del DateTime																							
*/


