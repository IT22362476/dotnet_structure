using FluentValidation;


namespace Inv.Application.Features.UOMConversion.Queries
{
    public class GetUomConversionsWithPaginationValidator : AbstractValidator<GetUomConversionsWithPaginationQuery>
    {
        public GetUomConversionsWithPaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize at least greater than or equal to 1.");

        }
    }
}
