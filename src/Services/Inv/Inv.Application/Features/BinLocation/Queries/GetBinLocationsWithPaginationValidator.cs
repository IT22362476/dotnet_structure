using FluentValidation;
using Inv.Application.Features.Item.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.BinLocation.Queries
{
     public class GetBinLocationsWithPaginationValidator : AbstractValidator<GetBinLocationWithPaginationQuery>
    {
        public GetBinLocationsWithPaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber is required.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize is required.");
            RuleFor(x => x.SortColumn)
                .NotNull()
                .NotEmpty()
                .WithMessage("Column is required.");
            RuleFor(x => x.SortDirection)
                 .NotNull()
                .NotEmpty()
                .WithMessage("Sort direction is required.");
        }
    }
}
