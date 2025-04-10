using FluentValidation;
using Inv.Application.Features.BrandItemType.Command;
using Inv.Application.Interfaces.Repositories;
using INV.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.Rack.Command
{
    public class UpdateRackValidator : AbstractValidator<UpdateRackCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRackValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x)
                   .MustAsync(AreDimensionsGreaterThanExistingAsync)
                   .WithMessage("The new dimensions must be greater than the existing dimensions.");

            RuleFor(x => x.Rows)
                .NotEmpty()
                .GreaterThan(1)
                .NotNull()
                .WithMessage("The rows are required.");

            RuleFor(x => x.Columns)
                .NotEmpty()
                .GreaterThan(1)
                .NotNull()
                .WithMessage("The columns are required.");

            RuleFor(x => x.Compartments)
                .GreaterThan(1)
                .NotEmpty()
                .NotNull()
                .WithMessage("The compartments are required.");
        }
        private async Task<bool> AreDimensionsGreaterThanExistingAsync(UpdateRackCommand command, CancellationToken cancellationToken)
        {
            // Fetch the existing rack
            var existingRack = await _unitOfWork.Repository<Domain.Entities.Rack>().GetByIdAsync(command.RackSerialID);

            if (existingRack == null)
            {
                throw new InvalidOperationException("No existing dimensions found.");
            }

            // Compare the new dimensions with the existing ones
            return command.Rows >= existingRack.Rows &&
                   command.Columns >= existingRack.Columns &&
                   command.Compartments >= existingRack.Compartments;
        }
    }
}
