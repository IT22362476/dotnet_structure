using Microsoft.EntityFrameworkCore.Storage;

namespace Inv.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task<int> Save(CancellationToken cancellationToken);
        Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);
        Task Rollback();
        //new test code
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitAsync();
        Task RollbackAsync();
        Task<int> SaveNoCommitRoll(CancellationToken cancellationToken);
    }
}
