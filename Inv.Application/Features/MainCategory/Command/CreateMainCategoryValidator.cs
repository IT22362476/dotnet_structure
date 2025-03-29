using FluentValidation;
using Inv.Application.DTOs.MainCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.MainCategory.Command
{
    public class CreateMainCategoryValidator : AbstractValidator<CreateMainCategoryDto>
    {
        public CreateMainCategoryValidator()
        {
            RuleFor(x => x.MainCategoryName)
               .NotEmpty().WithMessage("MainCategoryName can not be empty")
               .NotNull().WithMessage("MainCategoryName can not be null");

         
        }
    }
}
