using FluentValidation;
using Inv.Application.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.Brand.Queries
{
    public class GetBrandValidator : AbstractValidator<GetBrandDto>
    {
        public GetBrandValidator()
        {
            RuleFor(x => x.BrandSerialID)
                .NotNull().WithMessage("BrandSerialID can not be null")
                .GreaterThanOrEqualTo(1).WithMessage("BrandSerialID  must be greater than or equal to 1.");

            RuleFor(x => x.BrandName)
                .NotEmpty().WithMessage("BrandName can not be empty")
                .NotNull().WithMessage("BrandName can not be null");
        }
    }
}
