using FluentValidation;
using Inv.Application.DTOs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.Model.Command
{
    public class CreateModelValidator : AbstractValidator<CreateModelDto>
    {
        public CreateModelValidator()
        {
            RuleFor(x => x.ModelName)
              .NotEmpty().WithMessage("ModelName can not be empty")
              .NotNull().WithMessage("ModelName can not be null");

            RuleFor(x => x.BrandSerialID)
              .GreaterThanOrEqualTo(1).WithMessage("BrandSerialID  must be greater than or equal to 1.")
              .NotNull().WithMessage("BrandSerialID can not be null");
        }
    }
}
