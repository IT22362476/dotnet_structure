using Dapper;
using FluentValidation;
using System.Data;
using Inv.Application.DTOs.GRN;
using Inv.Application.Extensions;
using Inv.Application.Interfaces.Repositories;
using Inv.Application.Utility;
using Inv.Shared;
using MediatR;

namespace Inv.Application.Features.GRN.Queries
{
    public class GetGRNsWithPaginationQuery : IRequest<Result<PaginatedResult<GetPaginatedGRNHeadersDto>>>
    {
        public GetGRNsWithPaginationQuery()
        {
        }

        public GetGRNsWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? SortColumn { get; set; } // Column to sort by
        public string? SortDirection { get; set; } // Sort direction: ASC or DESC
    }

    public class GetGRNsWithPaginationQueryHandler : IRequestHandler<GetGRNsWithPaginationQuery, Result<PaginatedResult<GetPaginatedGRNHeadersDto>>>
    {
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetPaginatedGRNHeadersDto> _sortHelper;

        public GetGRNsWithPaginationQueryHandler(ISqlDataAccess sqlDataAccess, ISortHelper<GetPaginatedGRNHeadersDto> sortHelper)
        {
            _sqlDataAccess = sqlDataAccess;
            _sortHelper = sortHelper;
        }

        public async Task<Result<PaginatedResult<GetPaginatedGRNHeadersDto>>> Handle(GetGRNsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            // Validate the request using FluentValidation
            var validator = new GetGRNsWithPaginationQueryValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<PaginatedResult<GetPaginatedGRNHeadersDto>>.Failure(errors);
            }

            var parameters = new DynamicParameters();
            parameters.Add("@pageNumber", request.PageNumber, DbType.Int64, ParameterDirection.Input, request.PageNumber);
            parameters.Add("@pageSize", request.PageSize, DbType.Int64, ParameterDirection.Input, request.PageSize);

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
                        WHERE 1 = 1 AND grn.ApprovedBy IS NULL";

            // split the search terms by comma
            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                string[] terms = request.Filter.Split(',');
                foreach (string term in terms)
                {
                    // split each term into key and value
                    var parts = term.Split('=');
                    if (parts.Length == 2)
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
            string sortColumn = string.IsNullOrWhiteSpace(request.SortColumn) ? "GRNHeaderSerialID" : request.SortColumn; // default column
            string sortDirection = request.SortDirection?.ToUpper() == "DESC" ? "DESC" : "ASC"; // default to ascending
            sortColumn = SqlHelper.QuoteIdentifier(sortColumn); // Prevent SQL injection
            sql += $" ORDER BY {sortColumn} {sortDirection}";

            var data = _sqlDataAccess.LoadDataQuery<GetPaginatedGRNHeadersDto, dynamic>(sql, parameters).Result.AsEnumerable();

            var result = await data.ToPaginatedCustomListAsync(request.PageNumber, request.PageSize, cancellationToken);

            return await Result<PaginatedResult<GetPaginatedGRNHeadersDto>>.FailureAsync(result, "Loaded Successfully.");
        }

    }

}
