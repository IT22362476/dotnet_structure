using FluentValidation;
using Inv.Application.DTOs.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.SubCategory.Command
{
    public class UpdateSubCategoryValidator : AbstractValidator<UpdateSubCategoryDto>
    {
        public UpdateSubCategoryValidator()
        {
            RuleFor(x => x.SubCategorySerialID)
             .GreaterThanOrEqualTo(1).WithMessage("SubCategorySerialID  must be greater than or equal to 1.")
             .NotNull().WithMessage("SubCategorySerialID can not be null");

            RuleFor(x => x.SubCategoryName)
             .NotEmpty().WithMessage("SubCategoryName can not be empty")
             .NotNull().WithMessage("SubCategoryName can not be null");

            RuleFor(x => x.MainCategorySerialID)
              .GreaterThanOrEqualTo(1).WithMessage("MainCategorySerialID  must be greater than or equal to 1.")
              .NotNull().WithMessage("MainCategorySerialID can not be null");
        }
    }
}
