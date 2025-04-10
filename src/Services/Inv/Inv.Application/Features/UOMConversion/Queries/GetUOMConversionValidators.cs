using FluentValidation;
using Inv.Application.DTOs.UOMConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.UOMConversion.Queries
{
    public class GetUOMConversionValidators : AbstractValidator<GetUOMConversionDto>
    {
        public GetUOMConversionValidators()
        {
            RuleFor(x => x.UOMConvSerialID)
              .GreaterThanOrEqualTo(1).WithMessage("UOMConvSerialID  must be greater than or equal to 1.")
             .NotNull().WithMessage("UOMConvSerialID can not be null");

            RuleFor(x => x.UOMSerialID)
              .GreaterThanOrEqualTo(1).WithMessage("UOMSerialID  must be greater than or equal to 1.")
             .NotNull().WithMessage("UOMSerialID can not be null");

            RuleFor(x => x.UOMToID)
              .GreaterThanOrEqualTo(1).WithMessage("UOMToID  must be greater than or equal to 1.")
              .NotNull().WithMessage("UOMToID can not be null");


            RuleFor(x => x.ConversionRate)
             .NotEmpty().WithMessage("ConversionRate can not be empty")
             .NotNull().WithMessage("ConversionRate can not be null");
        }
    }
}
