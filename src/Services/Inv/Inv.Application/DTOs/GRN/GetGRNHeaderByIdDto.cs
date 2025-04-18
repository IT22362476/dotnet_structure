using System.ComponentModel.DataAnnotations;

namespace Inv.Application.DTOs.GRN
{
    public class GetGRNHeaderByIdDto
    {
        [Required]
        public int GRNHeaderSerialID { get; set; }

        [Required]
        public int GRNID { get; set; }

        [Required]
        public int CompSerialID { get; set; }

        [Required]
        public int SupplierSerialID { get; set; }

        [Required]
        public string? SupplierInvoiceNumber { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        [Required]
        public int StoreSerialID { get; set; }

        [Required]
        public bool Printed { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
