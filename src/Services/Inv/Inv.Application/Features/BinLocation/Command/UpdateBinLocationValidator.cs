using FluentValidation;
using Inv.Application.Features.Rack.Command;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Inv.Application.Features.BinLocation.Command
{
    public class UpdateBinLocationValidator : AbstractValidator<UpdateBinLocationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBinLocationValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x)
                .NotEmpty()
                .NotNull()
                .MustAsync(IsUniqueBinLctnAsync)
                .WithMessage("The bin location is unique.");

            RuleFor(x => x.Column)
                .NotEmpty()
                .NotNull()
                .WithMessage("The columns are required.");

            RuleFor(x => x.Compartment)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .WithMessage("The compartments are required.");
            RuleFor(x => x.Row)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .WithMessage("The compartments are required.");
        }
        private async Task<bool> IsUniqueBinLctnAsync(UpdateBinLocationCommand command, CancellationToken cancellationToken)
        {

            // Fetch the existing bin location from the database
            var existingBin = await _unitOfWork.Repository<Domain.Entities.BinLocation>().GetByIdAsync(command.BinLctnSerialID);

            if (existingBin == null)
            {
                throw new InvalidOperationException("No existing bin found.");
            }
            // Compare the new dimensions with the existing ones
            if ((command.Row >= existingBin.Row || command.Row <= existingBin.Row)
                && command.Column != existingBin.Column 
                && (command.Compartment >= existingBin.Compartment || command.Compartment <= existingBin.Compartment))
            {
                // verify if the new bin location in the request is unique in the context
                var result = await _unitOfWork.Repository<Domain.Entities.BinLocation>().Entities.Where(b => b.BinLctn == command.BinLctn).FirstOrDefaultAsync();
                if (result == null)
                {
                    return true;
                }
            }
            return true;
        }

    }
}
