using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Interfaces.Repositories
{
    public interface IPermissionService
    {
        Dictionary<string, int> GetPermissionsAsync();

    }
}
