using Microsoft.EntityFrameworkCore;
using Inv.Shared;
using System.Linq.Dynamic.Core;


namespace Inv.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken) where T : class
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;

            // Ensure pagination is correct
            int count = await source.CountAsync();
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return PaginatedResult<T>.Create(items, count, pageNumber, pageSize);
        }
    }
    public static class EnumarableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedCustomListAsync<T>(this IEnumerable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken) where T : class
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            // Ensure pagination is correct

            int count = source.Count();
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            List<T> items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return PaginatedResult<T>.Create(items, count, pageNumber, pageSize);
        }

    }

}
