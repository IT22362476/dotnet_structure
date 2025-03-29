using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Interfaces
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOnUtc { get; set; }
    }
}
