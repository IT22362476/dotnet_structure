using FluentValidation;

namespace Inv.Application.Features.GRN.Commands
{
    public class DeleteGRNCommandValidator : AbstractValidator<DeleteGRNCommand>
    {
        public DeleteGRNCommandValidator()
        {
            RuleFor(x => x.GRNHeaderSerialID)
              .NotEmpty()
              .NotNull()
              .WithMessage("The grn# is required.");

            RuleFor(x => x.DeletedBy)
                .GreaterThan(0)
               .NotEmpty()
               .NotNull()
               .WithMessage("The approver is required.");

        }
    }
}
