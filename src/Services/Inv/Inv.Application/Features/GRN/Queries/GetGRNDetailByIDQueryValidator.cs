using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Inv.Application.Features.GRN.Queries
{
    public class GetGRNDetailByIDQueryValidator : AbstractValidator<GetGRNDetailByIDQuery>
    {
        public GetGRNDetailByIDQueryValidator()
        {
            RuleFor(x => x.GRNDetailSerialID)
                .NotEmpty().WithMessage("GRN Detail Serial ID is required.")
                .GreaterThanOrEqualTo(1)
                .WithMessage("GRN Detail Serial ID at least greater than or equal to 1.");
        }
    }
}
