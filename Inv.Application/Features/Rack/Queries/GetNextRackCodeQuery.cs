using Inv.Application.Helper;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace Inv.Application.Features.Rack.Queries
{
    public class GetNextRackCodeQuery : IRequest<Result<string>>
    {
    }
    public class GetNextRackCodeQueryHandler : IRequestHandler<GetNextRackCodeQuery, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNextRackCodeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(GetNextRackCodeQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the highest RackCode
            var lastRackCode = await GetLastRackCodeAsync(cancellationToken);

            if (string.IsNullOrEmpty(lastRackCode))
            {
                return await Result<string>.FailureAsync(message: "No RackCode found.");
            }

            // Generate the next RackCode
            var nextRackCode = RackCodeGenerator.GetNextRackCode(lastRackCode);

            return await Result<string>.SuccessAsync(data: nextRackCode, message: "Successfully generated the next RackCode.");
        }
        // Helper method to encapsulate RackCode retrieval logic
        private async Task<string?> GetLastRackCodeAsync(CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Domain.Entities.Rack>().Entities.Select(r => r.RackCode).MaxAsync(cancellationToken);
        }
    }
}
