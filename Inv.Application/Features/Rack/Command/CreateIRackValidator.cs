using FluentValidation;
using Inv.Application.Interfaces.Repositories;


namespace Inv.Application.Features.Rack.Command
{
    public class CreateRackValidator : AbstractValidator<CreateRackCommand>
    {
        private readonly IRackRepository _rack;

        public CreateRackValidator(IRackRepository rack)
        {
            _rack = rack;

            RuleFor(x => x.RackCode)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("The rackcode is required.")
                    .MustAsync(IsUniqueRackCodeAsync)
                    .WithMessage("The rackcode is unique");

        }
        private async Task<bool> IsUniqueRackCodeAsync(string rackCode, CancellationToken cancellationToken)
        {
           bool isExistingRackCode = await _rack.IsUniqueRackCodeAsync(rackCode, cancellationToken);
           return isExistingRackCode;
        }
    }
}
