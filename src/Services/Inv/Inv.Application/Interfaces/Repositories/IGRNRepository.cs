using Inv.Application.Features.GRN.Commands;
using Inv.Application.Features.GRN.Queries;
using Inv.Domain.Entities;
using Inv.Shared;

namespace Inv.Application.Interfaces.Repositories
{
    public interface IGRNRepository
    {
        Task<GRNHeader?> GetGRNHeaderByIDAsync(GetGRNHeaderByIDQuery query, CancellationToken cancellationToken);
        Task<Result<int>> CreateGRNAsync(CreateGRNCommand request, CancellationToken cancellationToken);
        Task<Result<int>> DeleteGRNAsync(DeleteGRNCommand delete, CancellationToken cancellationToken);
        Task<Result<int>> ApproveGRNHeaderAsync(ApproveGRNCommand approve, CancellationToken cancellationToken);

    }
}
