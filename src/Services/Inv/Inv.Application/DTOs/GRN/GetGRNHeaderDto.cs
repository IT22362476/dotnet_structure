using System.ComponentModel.DataAnnotations;

namespace Inv.Application.DTOs.GRN
{
    public class GetGRNHeaderDto
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

        public string? SupplierName { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        public string? WHName { get; set; }

        [Required]
        public int StoreSerialID { get; set; }

        public string? StoreName { get; set; }

        [Required]
        public bool Printed { get; set; }

        [Required]
        public int PreparedBy { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }
    }
}
