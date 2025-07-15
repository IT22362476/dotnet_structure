using Inv.Application.Interfaces.Repositories;
using Inv.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Inv.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }
         public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            return entities;
        }
        public Task UpdateAsync(T entity, int id)
        {
            T exist = _dbContext.Set<T>().Find(id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
        public Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            return Task.CompletedTask;

        }
        public Task DeleteRangeAsync(IEnumerable<T> entities) // Implement DeleteRangeAsync method
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;

        }
       public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }
        public async Task<List<T>> GetAllFindAsync(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = _dbContext.Set<T>().IgnoreQueryFilters();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }
        public async Task<T> GetEntityWithThenIncludesAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<IQueryable<T>, IQueryable<T>>>[] includeExpressions)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            // Apply each include expression
            foreach (var includeExpression in includeExpressions)
            {
                query = includeExpression.Compile().Invoke(query);
            }

            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        // New AnyAsync method to check if any entity matches the condition
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate);
        }
        public async Task<T> GetEntityWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            foreach (var include in includes)
            {
                query = System.Data.Entity.QueryableExtensions.Include(query, include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }
        public void Detach(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }
        // Implementation of ExecuteScalar
        public T ExecuteScalar<T>(string sql, params SqlParameter[] parameters)
        {
            // Use the database connection to execute the SQL command
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                if (command.Connection.State != System.Data.ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                var result = command.ExecuteScalar();
                return (T)Convert.ChangeType(result, typeof(T));
            }
        }
    }
}


