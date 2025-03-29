using FluentValidation;
using Inv.Application.DTOs.ItemType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.ItemType.Queries
{
    public class GetUpdateItemTypeValidator : AbstractValidator<GetItemTypeDto>
    {
        public GetUpdateItemTypeValidator()
        {
            RuleFor(x => x.ItemTypeSerialID)
              .GreaterThanOrEqualTo(1).WithMessage("ItemTypeSerialID  must be greater than or equal to 1.")
              .NotNull().WithMessage("ItemTypeSerialID can not be null");

            RuleFor(x => x.ItemTypeName)
              .NotEmpty().WithMessage("ItemTypeName can not be empty")
              .NotNull().WithMessage("ItemTypeName can not be null");
          
        }
    }
}
