using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.UOM
{
    public class GetUomsWithPaginationDto
    {
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Approved { get; set; }
        public string? Modified { get; set; }
    }
}
