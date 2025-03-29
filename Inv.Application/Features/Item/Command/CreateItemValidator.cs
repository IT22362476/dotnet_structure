using FluentValidation;
using Inv.Application.DTOs.Item;
using Inv.Application.Interfaces.Repositories;
using INV.Application.Interfaces.Repositories;

namespace Inv.Application.Features.Item.Command
{
    public class CreateItemValidator : AbstractValidator<CreateItemCommand>
    {
        private readonly IItemRepository _item;

        public CreateItemValidator(IItemRepository item)
        {
            _item = item;

            RuleFor(x => x.ItemTypeSerialID)
                .NotEmpty().WithMessage("Item type can not be empty")
                .NotNull().WithMessage("Item type can not be null");

            RuleFor(x => x.MainCategorySerialID)
                .GreaterThanOrEqualTo(1).WithMessage("MainCategory can not be empty.")
                .NotNull().WithMessage("MainCategory can not be null");

            RuleFor(x => x.SubCategorySerialID)
                .GreaterThanOrEqualTo(1).WithMessage("Sub Category can not be empty.")
                .NotNull().WithMessage("Sub Category can not be null");

            RuleFor(x => x.BrandSerialID)
                .GreaterThanOrEqualTo(1).WithMessage("Brand can not be empty.")
                .NotNull().WithMessage("Brand can not be null");
            RuleFor(x => x.ModelSerialID)
                .GreaterThanOrEqualTo(1).WithMessage("Model can not be empty.")
                .NotNull().WithMessage("Model can not be null");
        }

    }
}
