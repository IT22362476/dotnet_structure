using FluentValidation;
using Inv.Application.DTOs.UOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.UOM.Command
{
    public class CreateUOMValidator : AbstractValidator<CreateUOMDto>
    {
        public CreateUOMValidator()
        {
            RuleFor(x => x.UOMName)
              .NotEmpty().WithMessage("UOMName can not be empty")
              .NotNull().WithMessage("UOMName can not be null");

            RuleFor(x => x.UOMDescription)
              .NotEmpty().WithMessage("UOMDescription can not be empty")
              .NotNull().WithMessage("UOMDescription can not be null");
        }
    }
}
