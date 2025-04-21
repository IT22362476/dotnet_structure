using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Inv.Application.Features.GRN.Queries
{
    public class GetGRNHeaderByIDQueryValidator : AbstractValidator<GetGRNHeaderByIDQuery>
    {
        public GetGRNHeaderByIDQueryValidator()
        {
            RuleFor(x => x.GRNHeaderSerialID)
                .NotEmpty().WithMessage("GRN Header Serial ID is required.")
                .GreaterThanOrEqualTo(1)
                .WithMessage("GRN Header Serial ID at least greater than or equal to 1.");
        }
    }
}
