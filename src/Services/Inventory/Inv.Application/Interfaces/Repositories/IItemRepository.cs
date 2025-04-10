using Inv.Application.DTOs.Item;
using Inv.Application.Features.Item.Command;
using Inv.Application.Request;
using Inv.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Interfaces.Repositories
{
    public interface IItemRepository
    {
        Task<Result<int>> CreateItemAsync(CreateItemCommand itemCommand, CancellationToken cancellationToken);
        Task<List<GetItemDto>> GetItemsAsync(EntityStatus entityStatus, CancellationToken cancellationToken);
        Task<bool> IsUniqueItemAsync(string itemCode, CancellationToken cancellationToken);
        Task<Result<int>> UpdateItemAsync(UpdateItemCommand updateItem, CancellationToken cancellationToken);

    }
}
