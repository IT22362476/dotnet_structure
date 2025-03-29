using FluentValidation;
using Inv.Application.Features.UOMConversion.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.Item.Queries
{
      public class GetItemsWithPaginationValidator : AbstractValidator<GetItemWithPaginationQuery>
    {
        public GetItemsWithPaginationValidator()
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
