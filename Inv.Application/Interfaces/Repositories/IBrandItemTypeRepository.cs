
using Inv.Application.DTOs.BrandItemType;
using Inv.Application.Features.BrandItemType.Command;
using Inv.Application.Request;
using Inv.Shared;

namespace INV.Application.Interfaces.Repositories
{
    public interface IBrandItemTypeRepository
    {
        Task<Result<int>> CreateBrandItemTypesAsync(CreateBrandItemTypeCommand brandItemTypeDto, CancellationToken cancellationToken);
        Task<List<GetBrandItemTypeDto>> GetBrandItemTypesAsync(EntityStatus entityStatus, CancellationToken cancellationToken);
        Task<bool> IsUniqueBrandAsync(string name, CancellationToken cancellationToken);
        Task<Result<int>> UpdateBrandItemTypesAsync(UpdateBrandItemTypeCommand brandItemTypeDto, CancellationToken cancellationToken);

    }
}
