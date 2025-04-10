using AutoMapper;
using Dapper;
using Inv.Application.DTOs.Rack;
using Inv.Application.Extensions;
using Inv.Application.Interfaces.Repositories;
using Inv.Application.Utility;
using Inv.Shared;
using MediatR;
using System.Data;

namespace Inv.Application.Features.Rack.Queries
{
    public record GetRackWithPaginationQuery : IRequest<Result<PaginatedResult<GetRacksWithPaginationDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? SortColumn { get; set; } // Column to sort by
        public string? SortDirection { get; set; } // Sort direction: ASC or DESC
        public GetRackWithPaginationQuery() { }

        public GetRackWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetRacksWithPaginationQueryHandler : IRequestHandler<GetRackWithPaginationQuery, Result<PaginatedResult<GetRacksWithPaginationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetRacksWithPaginationDto> _sortHelper;


        public GetRacksWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, ISortHelper<GetRacksWithPaginationDto> sortHelper)
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
        public async Task<Result<PaginatedResult<GetRacksWithPaginationDto>>> Handle(GetRackWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetRacksWithPaginationValidator validator = new GetRacksWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query);
            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
                parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);

                var sql = @"SELECT ROW_NUMBER() OVER(ORDER BY rk.RackSerialID) AS Id,rk.RackSerialID as RackSerialID,rk.RackName,rk.RackCode,rk.Rows,rk.Columns,rk.Compartments,rk.ComSerialID as ComSerialID,c.CompanyName as CompanyName,rk.WHSerialID as WHSerialID,w.WHName,rk.StoreSerialID as StoreSerialID,s.StoreName as Store,rk.ZoneSerialID as ZoneSerialID,z.ZoneName,rk.[CreatedBy],rk.[CreatedDate],rk.[ModifiedBy],rk.[ModifiedDate],rk.[Active],rk.[IsDeleted]
                            FROM [INVDB].[Inv].[Rack] AS rk
							left join [CoreDB].[Core].[Company] as c on rk.ComSerialID=c.ComSerialID
                            LEFT JOIN [INVDB].[Inv].[Warehouse] AS w ON rk.WHSerialID=w.WHSerialID 
                            LEFT JOIN [INVDB].[Inv].[Store] AS s ON rk.StoreSerialID=s.StoreSerialID
                            LEFT JOIN [INVDB].[Inv].[Zone] AS z ON rk.ZoneSerialID=z.ZoneSerialID
							LEFT JOIN [CoreDB].[Core].[User] AS usrm ON usrm.UserSerialID = rk.ModifiedBy
                            Where 1 =1";// Start with a true condition for dynamic filtering

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
                                        columnName = "rk.active";
                                        break;
                                    case "[ComSerialID]":
                                        columnName = "rk.[ComSerialID]";
                                        break;
                                    case "[WHSerialID]":
                                        columnName = "rk.[WHSerialID]";
                                        break;
                                    case "[ZoneSerialID]":
                                        columnName = "rk.[ZoneSerialID]";
                                        break;
                                    case "[CompanyName]":
                                        columnName = "c.[CompanyName]";
                                        break;
                                    case "[WHName]":
                                        columnName = "w.[WHName]";
                                        break;
                                    case "[ZoneName]":
                                        columnName = "z.[ZoneName]";
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
                string sortColumn = query.SortColumn ?? "RackSerialID"; // Default column
                string sortDirection = query.SortDirection?.ToUpper() == "DESC" ? "DESC" : "ASC"; // Default to ASC
                sortColumn = SqlHelper.QuoteIdentifier(sortColumn); // Prevent SQL injection
                sql += $" ORDER BY {sortColumn} {sortDirection};";
                var data = _sqlDataAccess.LoadDataQuery<GetRacksWithPaginationDto, dynamic>(sql, parameters).Result.AsEnumerable();

                var result = await data.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);

                return await Result<PaginatedResult<GetRacksWithPaginationDto>>.SuccessAsync(result, "Loaded Successfully.");
            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<PaginatedResult<GetRacksWithPaginationDto>>.FailureAsync(errors);

            }

        }
    }
 

}
