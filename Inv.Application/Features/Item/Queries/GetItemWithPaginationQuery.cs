using AutoMapper;
using Dapper;
using Inv.Application.DTOs.Item;
using Inv.Application.Extensions;
using Inv.Application.Interfaces.Repositories;
using Inv.Application.Utility;
using Inv.Shared;
using MediatR;
using System.Data;

namespace Inv.Application.Features.Item.Queries
{

    public record GetItemWithPaginationQuery : IRequest<Result<PaginatedResult<GetItemsWithPaginationDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? SortColumn { get; set; } // Column to sort by
        public string? SortDirection { get; set; } // Sort direction: ASC or DESC
        public GetItemWithPaginationQuery() { }

        public GetItemWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetItemsWithPaginationQueryHandler : IRequestHandler<GetItemWithPaginationQuery, Result<PaginatedResult<GetItemsWithPaginationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetItemsWithPaginationDto> _sortHelper;


        public GetItemsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, ISortHelper<GetItemsWithPaginationDto> sortHelper)
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
        public async Task<Result<PaginatedResult<GetItemsWithPaginationDto>>> Handle(GetItemWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetItemsWithPaginationValidator validator = new GetItemsWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query);
            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
                parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);

                var sql = @"SELECT ROW_NUMBER() OVER(ORDER BY itm.ItemSerialID) AS Id,itm.ItemSerialID as ItemSerialID,[ItemID],usr.UserName as Approved,usrm.UserName as Modified,[ItemCode],itm.[ItemTypeSerialID],itm.[ApprovedBy],itm.[ApprovedDate],ityp.ItemTypeName,b.BrandName+' '+m.ModelName+' '+c.MainCategoryName+' '+sc.SubCategoryName as [ItemDes],itm.[MainCategorySerialID],c.MainCategoryName,itm.[SubCategorySerialID],sc.SubCategoryName,itm.[ModelSerialID],m.ModelName,itm.[BrandSerialID],b.BrandName,[Weight],[Volume],[Size],[Color],[ItemPartNo],[Article],[Remarks],[Length],[Width],[Height],[Guage],[Construction],[SpecialFeatures],itm.[UOMSerialID],u.UOMName,itm.[CreatedBy],itm.[CreatedDate],itm.[ModifiedBy],itm.[ModifiedDate],itm.[Active],itm.[IsDeleted]
                            FROM [INVDB].[Inv].[Item] AS itm
                            LEFT JOIN [INVDB].[Inv].[ItemType] AS ityp ON itm.ItemTypeSerialID = ityp.ItemTypeSerialID
                            LEFT JOIN [INVDB].[Inv].[Brand] AS b ON itm.BrandSerialID = b.BrandSerialID
                            LEFT JOIN [INVDB].[Inv].[Model] AS m ON itm.ModelSerialID = m.ModelSerialID
                            LEFT JOIN [INVDB].[Inv].[MainCategory] AS c ON itm.MainCategorySerialID = c.MainCategorySerialID
                            LEFT JOIN [INVDB].[Inv].[SubCategory] AS sc ON itm.SubCategorySerialID = sc.SubCategorySerialID
                            LEFT JOIN [INVDB].[Inv].[UOM] AS u ON itm.UOMSerialID = u.UOMSerialID
							LEFT JOIN [CoreDB].[Core].[User] AS usr ON usr.UserSerialID = itm.ApprovedBy
							LEFT JOIN [CoreDB].[Core].[User] AS usrm ON usrm.UserSerialID = itm.ModifiedBy
                            Where 1 = 1 and itm.ApprovedBy is null";// Start with a true condition for dynamic filtering

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
                                        columnName = "itm.active";
                                        break;
                                    case "[ItemDes]":
                                        columnName = "b.BrandName+' '+m.ModelName+' '+c.MainCategoryName+' '+sc.SubCategoryName"; 
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
                string sortColumn = query.SortColumn ?? "ItemSerialID"; // Default column
                string sortDirection = query.SortDirection?.ToUpper() == "DESC" ? "DESC" : "ASC"; // Default to ASC
                sortColumn = SqlHelper.QuoteIdentifier(sortColumn); // Prevent SQL injection
                sql += $" ORDER BY {sortColumn} {sortDirection};";

                var data = _sqlDataAccess.LoadDataQuery<GetItemsWithPaginationDto, dynamic>(sql, parameters).Result.AsEnumerable();

                var result = await data.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);

                return await Result<PaginatedResult<GetItemsWithPaginationDto>>.SuccessAsync(result, "Loaded Successfully.");
            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<PaginatedResult<GetItemsWithPaginationDto>>.FailureAsync(errors);

            }

        }
    }

}
