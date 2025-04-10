using Inv.Application.Features.GRN.Commands;
using Inv.Shared;

namespace Inv.Application.Interfaces.Repositories
{
    public interface IGRNRepository
    {
        Task<Result<int>> CreateGRNAsync(CreateGRNCommand request, CancellationToken cancellationToken);
    }
}
