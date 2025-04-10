using FluentValidation;
using Inv.Application.DTOs.GRN;

namespace Inv.Application.Features.GRN.Commands
{
    public class CreateGRNCommandValidator : AbstractValidator<CreateGRNCommand>
    {
        public CreateGRNCommandValidator()
        {
            RuleFor(x => x.CompSerialID)
                .NotEmpty().WithMessage("Company Serial ID is required.");

            RuleFor(x => x.SupplierSerialID)             
                .NotEmpty().WithMessage("Supplier Serial ID is required.");

            RuleFor(x => x.WHSerialID)
                .NotEmpty().WithMessage("Warehouse Serial ID is required.");

            RuleFor(x => x.StoreSerialID)
                .NotEmpty().WithMessage("Store Serial ID is required.");

            // Validate GRNDetails
            RuleFor(x => x.GRNDetails)
                .NotEmpty().WithMessage("At least on GRN Detail is required.")
                .Must(details => details.GroupBy(d => d.LineNumber).All(g => g.Count() == 1))
                .WithMessage("Duplicate line numbers detected.");

            RuleForEach(x => x.GRNDetails).SetValidator(new CreateGRNDetailDtoValidator());
        }
    }

    public class CreateGRNDetailDtoValidator : AbstractValidator<CreateGRNDetailDto>
    {
        public CreateGRNDetailDtoValidator()
        {
            RuleFor(x => x.LineNumber)
                .NotEmpty().WithMessage("Line Number is required.")
                .GreaterThan(0).WithMessage("Line Number must be greater than 0.");

            RuleFor(x => x.ItemSerialID)
                .NotEmpty().WithMessage("Items Serial ID is required.");

            RuleFor(x => x.SystemPOSerialID)
                .NotEmpty().WithMessage("System PO Serial ID is required.");

            RuleFor(x => x.BatchNumber)
                .NotEmpty().WithMessage("Batch Number is required.")
                .Length(1, 50).WithMessage("Batch Number must be between 1 and 50 characters.");

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(DateTime.Today).When(x => x.ExpiryDate.HasValue)
                .WithMessage("Expiry date must be in the future");

            RuleFor(x => x.WarrentyPeriod)
                .GreaterThanOrEqualTo(0).WithMessage("Warranty period cannot be negative");

            RuleFor(x => x.UOMSerialID)
                .NotEmpty().WithMessage("Unit of Measure Serial ID is required.");

            RuleFor(x => x.Condition)
                .NotEmpty().WithMessage("Condition is required.");

            RuleFor(x => x.Qty)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be positive");

            RuleFor(x => x.FOCQty)
                .GreaterThanOrEqualTo(0).WithMessage("Free of cost quantity cannot be negative");

            RuleFor(x => x.Remarks)
                .MaximumLength(100).WithMessage("Remarks cannot exceed 100 characters");
        }
    }
}
