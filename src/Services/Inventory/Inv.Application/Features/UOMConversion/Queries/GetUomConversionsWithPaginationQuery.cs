using AutoMapper;
using Inv.Application.Extensions;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using Dapper;
using MediatR;
using System.Data;
using Inv.Application.DTOs.UOMConversion;
using Inv.Application.Utility;


namespace Inv.Application.Features.UOMConversion.Queries
{
    public record GetUomConversionsWithPaginationQuery : IRequest<Result<PaginatedResult<GetUomConversionsWithPaginationDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? SortColumn { get; set; } // Column to sort by
        public string? SortDirection { get; set; } // Sort direction: ASC or DESC
        public GetUomConversionsWithPaginationQuery() { }

        public GetUomConversionsWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetAssetsWithPaginationQueryHandler : IRequestHandler<GetUomConversionsWithPaginationQuery, Result<PaginatedResult<GetUomConversionsWithPaginationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetUomConversionsWithPaginationDto> _sortHelper;


        public GetAssetsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, ISortHelper<GetUomConversionsWithPaginationDto> sortHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqlDataAccess = sqlDataAccess;
            _sortHelper = sortHelper;
        }
        // Method to capitalize the first letter of a string
        private static string CapitalizeFirstLetter(string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsUpper(str[0]))
                return str;

            return char.ToUpper(str[0]) + str.Substring(1);
        }
        public async Task<Result<PaginatedResult<GetUomConversionsWithPaginationDto>>> Handle(GetUomConversionsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetUomConversionsWithPaginationValidator validator = new GetUomConversionsWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query);
            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
                parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);

                var sql = @"SELECT ROW_NUMBER() OVER(ORDER BY ucn.UOMConvSerialID) AS Id,ucn.UOMConvSerialID as UOMConvSerialID,u.UOMName,u.UOMDescription,ucn.[UOMToID] as UOMToID,ucn.UOMSerialID as UOMSerialID,uu.UOMName as UOMNameTo,uu.UOMDescription as UOMDescriptionTo,ucn.[ConversionRate],ucn.[ConversionDescription], u.CreatedDate, u.ModifiedDate,ucn.Active
                            FROM [INVDB].[Inv].[UOMConversion] AS ucn
                            LEFT JOIN [INVDB].[Inv].[UOM] AS u ON ucn.UOMSerialID = u.UOMSerialID
							LEFT JOIN [INVDB].[Inv].[UOM] AS uu ON ucn.UOMToID = uu.UOMSerialID
							LEFT JOIN [CoreDB].[Core].[User] AS usrm ON usrm.UserSerialID = ucn.ModifiedBy
                            Where 1 = 1";// Start with a true condition for dynamic filtering

                // Split the search terms by comma
                if (!string.IsNullOrWhiteSpace(query.Filter))
                {
                    string[] terms = query.Filter.Split(',');
                    foreach (var term in terms)
                    {
                        // Split each term into key and value
                        var parts = term.Split('=');
                        if (parts.Length == 2)
                        {
                            // Get the column name and capitalize the first letter
                            string columnName = CapitalizeFirstLetter(parts[0].Trim());
                            string value = parts[1].Trim();
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                // Validate column name and prevent SQL injection
                                columnName = SqlHelper.QuoteIdentifier(columnName);
                                switch (columnName)
                                {
                                    case "[Active]":
                                        columnName = "UCN.active";
                                        break;
                                    case "[UOMDescription]":
                                        columnName = "u.UOMDescription";
                                        break;
                                    case "[UOMName]":
                                        columnName = "u.UOMName";
                                        break;
                                    case "[UOMDescriptionTo]":
                                        columnName = "uu.UOMDescription"; 
                                        break;
                                    case "[UOMNameTo]":
                                        columnName = "uu.UOMName";
                                        break;
                                    default:
                                        columnName = "ucn." + columnName;
                                        break;
                                 }
                                // Sanitize the value
                                value = value.Replace("'", "''"); // Escape single quotes
                                                                  // Use LIKE clause
                                sql += $" AND {columnName} LIKE '%{value}%'"; // Use LIKE
                            }

                        }
                    }
                }
                // Apply Sorting
                string sortColumn = query.SortColumn ?? "UOMConvSerialID"; // Default column
                string sortDirection = query.SortDirection?.ToUpper() == "DESC" ? "DESC" : "ASC"; // Default to ASC
                sortColumn = SqlHelper.QuoteIdentifier(sortColumn); // Prevent SQL injection
                sql += $" ORDER BY {sortColumn} {sortDirection};";

                var data = _sqlDataAccess.LoadDataQuery<GetUomConversionsWithPaginationDto, dynamic>(sql, parameters).Result.AsEnumerable();
            
                var result = await data.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);

                return await Result<PaginatedResult<GetUomConversionsWithPaginationDto>>.SuccessAsync(result, "Loaded Successfully.");
            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<PaginatedResult<GetUomConversionsWithPaginationDto>>.FailureAsync(errors);

            }

        }
    }
 
}
