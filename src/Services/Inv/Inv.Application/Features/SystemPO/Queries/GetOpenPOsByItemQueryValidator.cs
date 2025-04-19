using FluentValidation;

namespace Inv.Application.Features.SystemPO.Queries
{
    public class GetOpenPOsByItemQueryValidator : AbstractValidator<GetOpenPOsByItemQuery>
    {
        public GetOpenPOsByItemQueryValidator()
        {
            RuleFor(x => x.ItemSerialID)
                .NotEmpty()
                .WithMessage("Items Serila ID is required.")
                .NotNull()
                .WithMessage("Item Serila ID cannot be null.")
                .GreaterThan(0)
                .WithMessage("Item Serial ID must be greater than zero.");
        }
    }
}
