using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Interfaces.Repositories
{
    public interface ISortHelper<T>
    {
        IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString);
        IQueryable<T> SearchByName(IQueryable<T> entities, string searchTerm);
        IQueryable<T> FilterByName(IQueryable<T> source, string searchTerm);
    }
}
