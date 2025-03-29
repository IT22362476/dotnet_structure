using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Domain.Common.interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents);
    }
}
