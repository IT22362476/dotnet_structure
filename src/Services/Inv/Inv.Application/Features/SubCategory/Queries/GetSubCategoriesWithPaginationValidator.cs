using FluentValidation;

namespace Inv.Application.Features.SubCategory.Queries
{
    public class GetSubCategoriesWithPaginationValidator : AbstractValidator<GetSubCategoriesWithPaginationQuery>
    {
        public GetSubCategoriesWithPaginationValidator()
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
