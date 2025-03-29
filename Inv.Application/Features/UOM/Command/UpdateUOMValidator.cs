using FluentValidation;
using Inv.Application.DTOs.UOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.UOM.Command
{
    public class UpdateUOMValidator : AbstractValidator<UpdateUOMDto>
    {
        public UpdateUOMValidator()
        {
            RuleFor(x => x.UOMSerialID)
              .GreaterThanOrEqualTo(1).WithMessage("UOMSerialID  must be greater than or equal to 1.")
              .NotNull().WithMessage("UOMSerialID can not be null");

            RuleFor(x => x.UOMName)
              .NotEmpty().WithMessage("UOMName can not be empty")
              .NotNull().WithMessage("UOMName can not be null");

            RuleFor(x => x.UOMDescription)
              .NotEmpty().WithMessage("UOMDescription can not be empty")
              .NotNull().WithMessage("UOMDescription can not be null");
        }
    }
}
