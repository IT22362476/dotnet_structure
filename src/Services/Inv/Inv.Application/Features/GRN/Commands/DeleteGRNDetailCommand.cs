using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Inv.Application.Features.GRN.Commands
{
    public class DeleteGRNDetailCommand : IRequest<Result<int>>
    {
        public int GRNDetailSerialID { get; }
        public DeleteGRNDetailCommand(int grnDetailSerialID)
        {
            GRNDetailSerialID = grnDetailSerialID;
        }
    }

    internal class DeleteGRNDetailCommandHandler : IRequestHandler<DeleteGRNDetailCommand, Result<int>>
    {
        private readonly IGRNRepository _gRNRepository;

        public DeleteGRNDetailCommandHandler(IGRNRepository gRNRepository)
        {
            _gRNRepository = gRNRepository;
        }

        public async Task<Result<int>> Handle(DeleteGRNDetailCommand request, CancellationToken cancellationToken)
        {
            // Validate query using FluentValidation
            DeleteGRNDetailCommandValidator validator = new DeleteGRNDetailCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }

            return await _gRNRepository.DeleteGRNDetailAsync(request, cancellationToken);
        }
    }
}
