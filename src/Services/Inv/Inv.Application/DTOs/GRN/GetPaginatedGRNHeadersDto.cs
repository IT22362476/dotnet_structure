using System.ComponentModel.DataAnnotations;

namespace Inv.Application.DTOs.GRN
{
    public class GetPaginatedGRNHeadersDto
    {
        [Required]
        public int GRNHeaderSerialID { get; set; }

        [Required]
        public int GRNID { get; set; }

        [Required]
        public string? CompName { get; set; }

        [Required]
        public string? SupplierName { get; set; }

        [Required]
        public string? SupplierInvoiceNumber { get; set; }

        [Required]
        public string? StoreName { get; set; }

        [Required]
        public int GRNLines { get; set; }
    }
}
