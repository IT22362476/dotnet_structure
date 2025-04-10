using FluentValidation;
using Inv.Application.DTOs.MainCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.MainCategory.Queries
{
    public class GetMainCategoryValidator : AbstractValidator<GetMainCategoryDto>
    {
        public GetMainCategoryValidator()
        {
            RuleFor(x => x.MainCategorySerialID)
                .GreaterThanOrEqualTo(1).WithMessage("MainCategorySerialID  must be greater than or equal to 1.")
                .NotNull().WithMessage("MainCategorySerialID can not be null");

            RuleFor(x => x.MainCategoryName)
               .NotEmpty().WithMessage("MainCategoryName can not be empty")
               .NotNull().WithMessage("MainCategoryName can not be null");
        }
    }
}
