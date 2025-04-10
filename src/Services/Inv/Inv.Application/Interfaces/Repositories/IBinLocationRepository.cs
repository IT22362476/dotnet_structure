using Inv.Application.DTOs.BinLocation;
using Inv.Application.Features.BinLocation.Command;
using Inv.Application.Request;
using Inv.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Interfaces.Repositories
{
    public interface IBinLocationRepository
    {
        Task<Result<int>> CreateBinLocationAsync(CreateBinLocationCommand createBinLocation, CancellationToken cancellationToken);
        Task<List<GetBinLocationDto>> GetBinLocationAsync(EntityStatus entityStatus, CancellationToken cancellationToken);
        Task<bool> IsUniqueBinLocationAsync(string BinLocation, CancellationToken cancellationToken);
        Task<Result<int>> UpdateBinLocationAsync(UpdateBinLocationCommand updateBinLocation, CancellationToken cancellationToken);
        Task<bool> IsUniqueBinNameAsync(string binName, CancellationToken cancellationToken);

    }
}
