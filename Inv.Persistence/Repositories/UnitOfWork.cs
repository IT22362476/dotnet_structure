using Inv.Application.Interfaces.Repositories;
using Inv.Domain.Common;
using Inv.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;
using System.Data.Entity;

namespace Inv.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private IDbContextTransaction _currentTransaction;   
        private Hashtable _repositories;
        private bool disposed;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[type];
        }
        public Task Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        public async Task<int> SaveNoCommitRoll(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> Save(CancellationToken cancellationToken)
        {
            var executionStrategy = _dbContext.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    // Perform the database operations within the execution strategy and transaction
                    var result = await _dbContext.SaveChangesAsync(cancellationToken);

                    //throw new NotImplementedException(); //test

                    // Commit the transaction
                    await transaction.CommitAsync(cancellationToken);

                    return result;
                }
                catch (Exception)
                {
                    // Roll back the transaction in case of an error
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });

        }

        public Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    _dbContext.Dispose();
                }
            }
            // Dispose unmanaged resources
            disposed = true;
        }
        // new testing code

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null)
            {
                _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            }
            return _currentTransaction;
        }
        public async Task CommitAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
                _currentTransaction.Dispose();
                _currentTransaction = null; // Reset after commit
            }
        }
        public async Task RollbackAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                _currentTransaction.Dispose();
                _currentTransaction = null; // Reset after rollback
            }
        }

    }

}
