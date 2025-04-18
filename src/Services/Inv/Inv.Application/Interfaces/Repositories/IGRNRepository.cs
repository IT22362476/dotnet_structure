using Inv.Application.Features.GRN.Commands;
using Inv.Shared;

namespace Inv.Application.Interfaces.Repositories
{
    public interface IGRNRepository
    {
        Task<Result<int>> CreateGRNAsync(CreateGRNCommand request, CancellationToken cancellationToken);
        Task<Result<int>> DeleteGRNAsync(DeleteGRNCommand delete, CancellationToken cancellationToken);
        Task<Result<int>> ApproveGRNHeaderAsync(ApproveGRNCommand approve, CancellationToken cancellationToken);

    }
}
