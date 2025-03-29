using FluentValidation;
using Inv.Application.DTOs.ItemType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.ItemType.Command
{
    public class CreateItemTypeValidator : AbstractValidator<CreateItemTypeDto>
    {
        public CreateItemTypeValidator()
        {
            RuleFor(x => x.ItemTypeName)
               .NotEmpty().WithMessage("ItemTypeName can not be empty")
               .NotNull().WithMessage("ItemTypeName can not be null");
        }
    }
}
