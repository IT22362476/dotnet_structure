using FluentValidation;
using Inv.Application.DTOs.Item;
using Inv.Application.Interfaces.Repositories;

namespace Inv.Application.Features.Item.Command
{
     public class UpdateItemValidation : AbstractValidator<UpdateItemCommand>
    {
        private readonly IItemRepository _item;

        public UpdateItemValidation(IItemRepository item)
        {
            _item = item;

            RuleFor(x => x.ItemSerialID)
                .NotEmpty().WithMessage("Item type can not be empty")
                .NotNull().WithMessage("Item type can not be null");
        }
        private async Task<bool> IsUniqueItemAsync(string itemCode, CancellationToken cancellationToken)
        {
            bool isExistingUsername = await _item.IsUniqueItemAsync(itemCode, cancellationToken);
            return isExistingUsername;
        }
    }
}
