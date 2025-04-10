using Inv.Application.DTOs.Rack;
using Inv.Application.Features.BrandItemType.Command;
using Inv.Application.Features.Rack.Command;
using Inv.Application.Request;
using Inv.Shared;

namespace Inv.Application.Interfaces.Repositories
{
     public interface IRackRepository
    {
        Task<Result<int>> CreateRackAsync(CreateRackCommand createRack, CancellationToken cancellationToken);
        Task<List<GetRackDto>> GetRackAsync(EntityStatus entityStatus, CancellationToken cancellationToken);
        Task<bool> IsUniqueRackCodeAsync(string rackCode, CancellationToken cancellationToken);
        Task<Result<int>> UpdateRackAsync(UpdateRackCommand updateRack, CancellationToken cancellationToken);

    }
}
