using FluentValidation;
using Inv.Application.DTOs.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.Item.Queries
{
    public class GetItemValidator : AbstractValidator<GetItemDto>
    {
        public GetItemValidator()
        {
            RuleFor(x => x.ItemSerialID)
               .NotEmpty().WithMessage("ItemSerialID can not be empty")
               .NotNull().WithMessage("ItemSerialID can not be null");

            RuleFor(x => x.ItemCode)
                .NotEmpty().WithMessage("ItemCode can not be empty")
                .NotNull().WithMessage("ItemCode can not be null");

            RuleFor(x => x.ItemTypeSerialID)
                .GreaterThanOrEqualTo(1).WithMessage("ItemTypeSerialID  must be greater than or equal to 1.")
                .NotNull().WithMessage("ItemTypeSerialID can not be null");

            RuleFor(x => x.ItemDes)
                .NotEmpty().WithMessage("ItemDes can not be empty")
                .NotNull().WithMessage("ItemDes can not be null");

            RuleFor(x => x.MainCategorySerialID)
                .GreaterThanOrEqualTo(1).WithMessage("MainCategorySerialID  must be greater than or equal to 1.")
                .NotNull().WithMessage("MainCategorySerialID can not be null");

            RuleFor(x => x.SubCategorySerialID)
                .GreaterThanOrEqualTo(1).WithMessage("SubCategorySerialID  must be greater than or equal to 1.")
                .NotNull().WithMessage("SubCategorySerialID can not be null");

            RuleFor(x => x.ModelSerialID)
                .GreaterThanOrEqualTo(1).WithMessage("ModelSerialID  must be greater than or equal to 1.")
                .NotNull().WithMessage("ModelSerialID can not be null");

            RuleFor(x => x.Weight)
               .NotEmpty().WithMessage("Weight can not be empty")
               .NotNull().WithMessage("Weight can not be null");

            RuleFor(x => x.Volume)
               .NotEmpty().WithMessage("Volume can not be empty")
               .NotNull().WithMessage("Volume can not be null");

            RuleFor(x => x.Size)
               .NotEmpty().WithMessage("Size can not be empty")
               .NotNull().WithMessage("Size can not be null");

            RuleFor(x => x.Color)
              .NotEmpty().WithMessage("Color can not be empty")
              .NotNull().WithMessage("Color can not be null");

            RuleFor(x => x.Article)
              .NotEmpty().WithMessage("Article can not be empty")
              .NotNull().WithMessage("Article can not be null");

            RuleFor(x => x.Remarks)
             .NotEmpty().WithMessage("Remarks can not be empty")
             .NotNull().WithMessage("Remarks can not be null");
        }
    }
}
