using System.Data;
using Dapper;
using Inv.Application.DTOs.GRN;
using Inv.Application.Extensions;
using Inv.Application.Interfaces.Repositories;
using Inv.Application.Utility;
using Inv.Shared;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Inv.Application.Features.GRN.Queries
{
    public class GetApprovedGRNsWithPaginationQuery : IRequest<Result<PaginatedResult<GetPaginatedGRNHeadersDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? SortColumn { get; set; } // Column to sort by
        public string? SortDirection { get; set; } // Sort direction: ASC or DESC
        public int? CompSerialID { get; set; }

        public GetApprovedGRNsWithPaginationQuery()
        {
        }

        public GetApprovedGRNsWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }  

    }

    internal class GetApprovedGRNsWithPaginationQueryHandler : IRequestHandler<GetApprovedGRNsWithPaginationQuery, Result<PaginatedResult<GetPaginatedGRNHeadersDto>>>
    {
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetPaginatedGRNHeadersDto> _sortHelper;

        public GetApprovedGRNsWithPaginationQueryHandler(ISqlDataAccess sqlDataAccess, ISortHelper<GetPaginatedGRNHeadersDto> sortHelper)
        {
            _sqlDataAccess = sqlDataAccess;
            _sortHelper = sortHelper;
        }

        public async Task<Result<PaginatedResult<GetPaginatedGRNHeadersDto>>> Handle(GetApprovedGRNsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Validate the request using FluentValidation
            var validator = new GetApprovedGRNsWithPaginationQueryValidator();
            var validationResult = validator.Validate(query);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<PaginatedResult<GetPaginatedGRNHeadersDto>>.Failure(errors);
            }

            var parameters = new DynamicParameters();
            parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
            parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);
            parameters.Add("@fromCompID", query.CompSerialID, DbType.Int64, ParameterDirection.Input, query.CompSerialID);

            var sql = @"
                        SELECT 
	                        ROW_NUMBER() OVER(ORDER BY grn.GRNHeaderSerialID) AS Id,
	                        grn.GRNHeaderSerialID,
	                        grn.GRNID,
	                        comp.CompanyName AS CompName,
	                        supplier.SupplierName,
	                        grn.SupplierInvoiceNumber,
	                        store.StoreName,
	                        COUNT(detail.GRNDetailSerialID) AS GRNLines
                        FROM [INVDB].[Inv].[GRNHeader] AS grn
                        LEFT JOIN [CoreDB].[Core].[Company] AS comp ON comp.ComSerialID = grn.CompSerialID
                        LEFT JOIN [INVDB].[Inv].[Supplier] AS supplier ON supplier.SupplierSerialId = grn.SupplierSerialID
                        LEFT JOIN [INVDB].[Inv].Store AS store ON store.StoreSerialID = grn.StoreSerialID
                        LEFT JOIN [CoreDB].[Core].[User] AS usr ON usr.UserSerialID = grn.ApprovedBy
                        LEFT JOIN [CoreDB].[Core].[User] AS usrm ON usrm.UserSerialID = grn.PreparedBy
                        LEFT JOIN [INVDB].[Inv].[GRNDetail] AS detail ON detail.GRNHeaderSerialID = grn.GRNHeaderSerialID
                        WHERE 1 = 1 AND grn.CompSerialID=@fromCompID AND grn.ApprovedBy IS NOT NULL AND grn.IsDeleted = 0";

            // split the search terms by comma
            if (!string.IsNullOrWhiteSpace(query.Filter))
            {
                string[] terms = query.Filter.Split(',');
                foreach(string term in terms)
                {
                    // split each term into key and value
                    var parts = term.Split('=');
                    if(parts.Length == 2)
                    {
                        // Get column name and capitalize first letter
                        var columnName = HelperFunctions.CapitalizeFirstLetter(parts[0]).Trim();
                        string value = parts[1].Trim();
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            columnName = SqlHelper.QuoteIdentifier(columnName);
                            switch (columnName)
                            {
                                case "[Active]":
                                    columnName = "grn.Active";
                                    break;

                                case "[IsDeleted]":
                                    columnName = "grn.IsDeleted";
                                    break;

                                case "[CompSerialID]":
                                    columnName = "grn.CompSerialID";
                                    break;
                            }
                        }

                        // Sanitize the value
                        value = value.Replace("'", "''"); // Escape single quotes
                        sql += $" AND {columnName} LIKE '%{value}%'"; // Use Like clause
                    }
                }
            }

            sql += @" GROUP BY
                            grn.GRNHeaderSerialID,
                            grn.GRNID,
                            comp.CompanyName,
                            supplier.SupplierName,
                            grn.SupplierInvoiceNumber,
                            store.StoreName";

            // Apply sorting
            string sortColumn = string.IsNullOrWhiteSpace(query.SortColumn) ? "GRNHeaderSerialID" : query.SortColumn; // default column
            string sortDirection = query.SortDirection?.ToUpper() == "DESC" ? "DESC" : "ASC"; // default to ascending
            sortColumn = SqlHelper.QuoteIdentifier(sortColumn); // Prevent SQL injection
            sql += $" ORDER BY {sortColumn} {sortDirection}";

            var data = _sqlDataAccess.LoadDataQuery<GetPaginatedGRNHeadersDto, dynamic>(sql, parameters).Result.AsEnumerable();

            var result = await data.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);

            return await Result<PaginatedResult<GetPaginatedGRNHeadersDto>>.SuccessAsync(result, "Loaded Successfully.");
        }
    }
}
