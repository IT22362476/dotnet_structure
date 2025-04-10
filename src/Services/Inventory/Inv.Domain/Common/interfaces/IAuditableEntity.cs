using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Domain.Common.interfaces
{
    public interface IAuditableEntity
    {
        int? CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        int? ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        // string? IpAddress { get; set; }
        bool Active { get; set; }
        bool IsDeleted { get; set; }
    }
}
