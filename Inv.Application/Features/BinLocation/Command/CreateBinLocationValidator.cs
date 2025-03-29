using FluentValidation;
using Inv.Application.Features.Rack.Command;
using Inv.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.BinLocation.Command
{
    public class CreateBinLocationValidator : AbstractValidator<CreateBinLocationCommand>
    {
        private readonly IBinLocationRepository _bin;


        public CreateBinLocationValidator(IBinLocationRepository bin)
        {
            _bin = bin;

            RuleFor(x => x.BinLctn)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("The binlocation is required.")
                    .MustAsync(IsUniqueBinLocationAsync)
                    .WithMessage("The binlocation is unique");

            RuleFor(x => x.BinName)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("The binname is required.")
                    .MustAsync(IsUniqueBinNameAsync)
                    .WithMessage("The binname is unique");

        }
        private async Task<bool> IsUniqueBinLocationAsync(string binLctn, CancellationToken cancellationToken)
        {
            bool isExisting = await _bin.IsUniqueBinLocationAsync(binLctn, cancellationToken);
            return isExisting;
        }
        private async Task<bool> IsUniqueBinNameAsync(string binLctn, CancellationToken cancellationToken)
        {
            bool isExisting = await _bin.IsUniqueBinNameAsync(binLctn, cancellationToken);
            return isExisting;
        }
    }
}
