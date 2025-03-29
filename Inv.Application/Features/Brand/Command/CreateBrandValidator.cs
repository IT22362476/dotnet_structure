using FluentValidation;
using Inv.Application.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.Brand.Command
{
    public class CreateBrandValidator : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandValidator()
        {
            RuleFor(x => x.BrandName)
                .NotEmpty().WithMessage("BrandName can not be empty")
                .NotNull().WithMessage("BrandName can not be null");
        }
    }
}
