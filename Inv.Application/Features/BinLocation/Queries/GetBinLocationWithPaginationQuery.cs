using AutoMapper;
using Dapper;
using Inv.Application.DTOs.BinLocation;
using Inv.Application.DTOs.Rack;
using Inv.Application.Extensions;
using Inv.Application.Features.Rack.Queries;
using Inv.Application.Interfaces.Repositories;
using Inv.Application.Utility;
using Inv.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.BinLocation.Queries
{
    public record GetBinLocationWithPaginationQuery : IRequest<Result<PaginatedResult<GetBinLocationsWithPaginationDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? SortColumn { get; set; } // Column to sort by
        public string? SortDirection { get; set; } // Sort direction: ASC or DESC
        public GetBinLocationWithPaginationQuery() { }

        public GetBinLocationWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetRacksWithPaginationQueryHandler : IRequestHandler<GetBinLocationWithPaginationQuery, Result<PaginatedResult<GetBinLocationsWithPaginationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetBinLocationsWithPaginationDto> _sortHelper;


        public GetRacksWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, ISortHelper<GetBinLocationsWithPaginationDto> sortHelper)
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
        public async Task<Result<PaginatedResult<GetBinLocationsWithPaginationDto>>> Handle(GetBinLocationWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetBinLocationsWithPaginationValidator validator = new GetBinLocationsWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query);
            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
                parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);

                var sql = @"SELECT ROW_NUMBER() OVER(ORDER BY bl.BinLctnSerialID) AS Id,bl.BinLctnSerialID as BinLctnSerialID,rk.RackSerialID,rk.RackName,rk.RackCode,rk.Rows,rk.Columns,rk.Compartments,bl.Row,bl.[Column],bl.Compartment,bl.BinLctn,bl.ComSerialID as ComSerialID,c.CompanyName as CompanyName,bl.WHSerialID as WHSerialID,w.WHName,bl.StoreSerialID as StoreSerialID,s.StoreName,bl.ZoneSerialID as ZoneSerialID,z.ZoneName,bl.BinName,bl.ItemSerialID as ItemSerialID,itm.ItemCode,itm.ItemPartNo,bl.[CreatedBy],bl.[CreatedDate],bl.[ModifiedBy],bl.[ModifiedDate],bl.[Active],bl.[IsDeleted]
                            FROM [INVDB].[Inv].[BinLocation] AS bl
							left join [CoreDB].[Core].[Company] as c on bl.ComSerialID=c.ComSerialID
                            LEFT JOIN [INVDB].[Inv].[Warehouse] AS w ON bl.WHSerialID=w.WHSerialID 
                            LEFT JOIN [INVDB].[Inv].[Store] AS s ON bl.StoreSerialID=s.StoreSerialID
                            LEFT JOIN [INVDB].[Inv].[Zone] AS z ON bl.ZoneSerialID=z.ZoneSerialID 
							left join [INVDB].[Inv].[Rack] AS rk on bl.RackSerialID=rk.RackSerialID
							left join [INVDB].[Inv].[Item] AS itm on bl.ItemSerialID=itm.ItemSerialID
							LEFT JOIN [CoreDB].[Core].[User] AS usrm ON usrm.UserSerialID = bl.ModifiedBy
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
                                        columnName = "bl.active";
                                        break;
                                    case "[ComSerialID]":
                                        columnName = "bl.[ComSerialID]";
                                        break;
                                    case "[WHSerialID]":
                                        columnName = "bl.[WHSerialID]";
                                        break;
                                    case "[ZoneSerialID]":
                                        columnName = "bl.[ZoneSerialID]";
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
                                    case "[ItemSerialID]":
                                        columnName = "itm.[ItemSerialID]";
                                        break;
                                    case "[RackSerialID]":
                                        columnName = "rk.[RackSerialID]";
                                        break;
                                    case "[RackCode]":
                                        columnName = "rk.[RackCode]";
                                        break;
                                        
                                    case "[RackName]":
                                        columnName = "rk.[RackName]";
                                        break;
                                    case "[ItemCode]":
                                        columnName = "itm.[ItemCode]";
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
                string sortColumn = query.SortColumn ?? "BinLctnSerialID"; // Default column
                string sortDirection = query.SortDirection?.ToUpper() == "DESC" ? "DESC" : "ASC"; // Default to ASC
                sortColumn = SqlHelper.QuoteIdentifier(sortColumn); // Prevent SQL injection
                sql += $" ORDER BY {sortColumn} {sortDirection};";
                var data = _sqlDataAccess.LoadDataQuery<GetBinLocationsWithPaginationDto, dynamic>(sql, parameters).Result.AsEnumerable();

                var result = await data.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);

                return await Result<PaginatedResult<GetBinLocationsWithPaginationDto>>.SuccessAsync(result, "Loaded Successfully.");
            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<PaginatedResult<GetBinLocationsWithPaginationDto>>.FailureAsync(errors);

            }

        }
    }


}
