using FluentValidation;

namespace Inv.Application.Features.GRN.Commands
{
    public class DeleteGRNDetailCommandValidator : AbstractValidator<DeleteGRNDetailCommand>
    {
        public DeleteGRNDetailCommandValidator()
        {
            RuleFor(x => x.GRNDetailSerialID)
                .NotEmpty().WithMessage("GRN Detail Serial ID is required.")
                .GreaterThanOrEqualTo(1)
                .WithMessage("GRN Detail Serial ID at least greater than or equal to 1.");
        }
    }
}
