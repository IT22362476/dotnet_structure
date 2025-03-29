using Inv.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using LinqKit;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inv.Persistence.Repositories
{
    public class SortHelper<T> : ISortHelper<T>
    {
        public IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString)
        {
            if (!entities.Any())
                return entities;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return entities;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return entities.OrderBy(orderQuery);
        }
        public IQueryable<T> SearchByName(IQueryable<T> entities, string searchTerm)
        {
            if (!entities.Any() || string.IsNullOrWhiteSpace(searchTerm))
                return entities;

            var orderParams = searchTerm.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var predicate = PredicateBuilder.New<T>(true);

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split("=")[0];
                var propertyFromQueryValue = param.Split("=")[1];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var parameter = Expression.Parameter(typeof(T), "o");
                var propertyAccess = Expression.Property(parameter, objectProperty);
                var propertyType = objectProperty.PropertyType;
                var searchValue = Convert.ChangeType(propertyFromQueryValue, propertyType);

                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var body = Expression.Call(propertyAccess, containsMethod, Expression.Constant(searchValue));

                predicate = predicate.And(Expression.Lambda<Func<T, bool>>(body, parameter));
            }

            return entities.Where(predicate);
        }
        public IQueryable<T> FilterByName(IQueryable<T> source, string searchTerm)
        {
            // If source not exists or searchTerm is null or whitespace, return paginated result
            if (!source.Any() || string.IsNullOrWhiteSpace(searchTerm))
                return source;

            // search column name and filter value are separating
            var orderParams = searchTerm.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var predicate = PredicateBuilder.New<T>(true);

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                // The search column name and filter value have been separated by equal sign

                var propertyFromQueryName = param.Split("=")[0];
                var propertyFromQueryValue = param.Split("=")[1];

                // The search column name if it exists in the SQL table

                // var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                // Exclude properties with the NotMapped attribute

                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase) &&
                        !pi.GetCustomAttributes(typeof(NotMappedAttribute), true).Any());

                // The search column name, if it  does not exist in the SQL table
                if (objectProperty == null)
                    continue;

                var parameter = Expression.Parameter(typeof(T), "o");
                var propertyAccess = Expression.Property(parameter, objectProperty);
                var propertyType = objectProperty.PropertyType;
                var searchValue1 = Convert.ChangeType(propertyFromQueryValue, propertyType);

                Expression body = null;

                if (propertyType == typeof(string))
                {
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    body = Expression.Call(propertyAccess, containsMethod, Expression.Constant(searchValue1));
                }
                else if (propertyType == typeof(int) || propertyType == typeof(int?))
                {
                    var searchValue = Convert.ToInt32(propertyFromQueryValue);
                    body = Expression.Equal(propertyAccess, Expression.Constant(searchValue));
                }
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                {
                    var searchValue = Convert.ToBoolean(propertyFromQueryValue);
                    body = Expression.Equal(propertyAccess, Expression.Constant(searchValue));
                }
                else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                {
                    // Example date range search term: "2023-01-01_2023-12-31"
                    var dateRange = propertyFromQueryValue.Split('_');
                    if (dateRange.Length == 2)
                    {

                        if (DateTime.TryParse(dateRange[0], out var startDate) && DateTime.TryParse(dateRange[1], out var endDate))
                        {
                            var startDateConstant = Expression.Constant((DateTime?)startDate, typeof(DateTime?));
                            var endDateConstant = Expression.Constant((DateTime?)endDate.AddDays(1), typeof(DateTime?));

                            // Handle the case where startDate and endDate are the same

                            var greaterThanOrEqual = Expression.GreaterThanOrEqual(propertyAccess, startDateConstant);
                            var lessThanOrEqual = Expression.LessThanOrEqual(propertyAccess, endDateConstant);

                            // Combine the two expressions using AndAlso, but ensure both are for nullable DateTime

                            body = Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);

                        }
                    }
                }

                if (body != null)
                {
                    //dynamically builds a composite predicate by combining multiple filtering conditions

                    predicate = predicate.And(Expression.Lambda<Func<T, bool>>(body, parameter));
                }
            }

            // Return paginated and filtered results
            var filteritems = source.Where(predicate);
            return filteritems;
        }

    }
}
