using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Request
{
    public class EntityStatus
    {
        public bool Active { get; set; } = true;
        public bool IsDelete { get; set; }= false;
        public bool IsApproved { get; set; } = false;
        public long? SerialID { get; set; }
        public string? UserID { get; set; }

    }
    public enum StatusType { Inactive = 0, Active = 1 }

}
