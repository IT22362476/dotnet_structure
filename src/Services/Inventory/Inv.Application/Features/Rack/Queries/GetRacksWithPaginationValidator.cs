using FluentValidation;
using Inv.Application.Features.Rack.Queries;


namespace Inv.Application.Features.Rack.Queries
{
      public class GetRacksWithPaginationValidator : AbstractValidator<GetRackWithPaginationQuery>
    {
        public GetRacksWithPaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize at least greater than or equal to 1.");
            RuleFor(x => x.SortColumn)
              .NotNull()
              .NotEmpty()
              .WithMessage("Column name can't be null or empty.");
            RuleFor(x => x.SortDirection)
               .NotNull()
               .NotEmpty()
               .WithMessage("Sorting direction can't be null or empty.");
        }
    }
}
