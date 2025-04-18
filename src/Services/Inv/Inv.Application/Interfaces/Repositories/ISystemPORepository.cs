using Inv.Domain.Entities;

namespace Inv.Application.Interfaces.Repositories
{
    public interface ISystemPORepository
    {
        Task<List<SystemPOHeader>> GetPOsWithDetails(List<int> poNumbers, CancellationToken cancellationToken);
    }
}
