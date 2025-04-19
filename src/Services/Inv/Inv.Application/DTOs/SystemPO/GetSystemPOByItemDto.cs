using System.ComponentModel.DataAnnotations;

namespace Inv.Application.DTOs.SystemPO
{
    public class GetSystemPOByItemDto
    {
        [Required]
        public int SystemPOHeaderSerialID { get; set; }

        [Required]
        public int SystemPOID { get; set; }

        [Required]
        public int SystemPODetailSerialID { get; set; }

        [Required]
        public int ItemSerialID { get; set; }

        [Required]
        public decimal BalToReceive { get; set; }
    }
}
