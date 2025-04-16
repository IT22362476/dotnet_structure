using Inv.Application.Interfaces.Repositories;
using Inv.Domain.Entities;
using Inv.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Inv.Persistence.Repositories
{
    public class SystemPORepository : ISystemPORepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SystemPORepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SystemPOHeader>> GetPOsWithDetails(List<int> poNumbers, CancellationToken cancellationToken)
        {
            var poList = await _dbContext
                .Set<SystemPOHeader>()
                .Include(x => x.SystemPODetails)
                .Where(x => poNumbers.Contains(x.SystemPOHeaderSerialID))
                .ToListAsync(cancellationToken);

            return poList;
        }
    }
}
