using Inv.Application.Features.Uoms.Queries;
using FluentValidation;

namespace Inv.Application.Features.Uom.Queries
{
    public class GetUomWithPaginationValidator : AbstractValidator<GetUomsWithPaginationQuery>
    {
        public GetUomWithPaginationValidator()
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
