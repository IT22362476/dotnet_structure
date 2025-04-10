using Inv.Domain.Common.interfaces;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Inv.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(List<T> entities);
        Task UpdateAsync(T entity, int id);
        Task DeleteAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task DeleteRangeAsync(IEnumerable<T> entities); // Define DeleteRangeAsync method
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllFindAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> GetEntityWithThenIncludesAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<IQueryable<T>, IQueryable<T>>>[] includeExpressions);
        Task<T> GetEntityWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        void Detach(T entity);
        T ExecuteScalar<T>(string sql, params SqlParameter[] parameters);

    }
}


