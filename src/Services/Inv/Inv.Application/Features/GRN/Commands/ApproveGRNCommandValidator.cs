using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Inv.Application.Features.GRN.Commands
{
    public class ApproveGRNCommandValidator : AbstractValidator<ApproveGRNCommand>
    {
        public ApproveGRNCommandValidator()
        {
            RuleFor(x => x.GRNHeaderSerialID)
              .NotEmpty()
              .NotNull()
              .WithMessage("The grn# is required.");

            RuleFor(x => x.ApprovedBy)
                .GreaterThan(0)
               .NotEmpty()
               .NotNull()
               .WithMessage("The approver is required.");

        }
    }
}
