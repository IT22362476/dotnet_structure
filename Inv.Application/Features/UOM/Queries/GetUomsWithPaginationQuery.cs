using AutoMapper;
using Inv.Application.Extensions;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using Dapper;
using MediatR;
using System.Data;
using Inv.Application.DTOs.UOM;
using Inv.Application.Features.Uom.Queries;
using Inv.Application.Utility;

namespace Inv.Application.Features.Uoms.Queries
{
    public record GetUomsWithPaginationQuery : IRequest<Result<PaginatedResult<GetUomsWithPaginationDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? SortColumn { get; set; } // Column to sort by
        public string? SortDirection { get; set; } // Sort direction: ASC or DESC
        public GetUomsWithPaginationQuery() { }

        public GetUomsWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetUomsWithPaginationQueryHandler : IRequestHandler<GetUomsWithPaginationQuery, Result<PaginatedResult<GetUomsWithPaginationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetUomsWithPaginationDto> _sortHelper;


        public GetUomsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, ISortHelper<GetUomsWithPaginationDto> sortHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqlDataAccess = sqlDataAccess;
            _sortHelper = sortHelper;
        }

        public async Task<Result<PaginatedResult<GetUomsWithPaginationDto>>> Handle(GetUomsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetUomWithPaginationValidator validator = new GetUomWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query);

            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
                parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);
                parameters.Add("@filteringCol", query.Filter, DbType.String, ParameterDirection.Input, 200);
                parameters.Add("@SortingCol", query.Filter, DbType.String, ParameterDirection.Input, 200);

                var sql = @"SELECT ROW_NUMBER() OVER(ORDER BY UOMSerialID) AS Id,* FROM [INVDB].[Inv].[UOM] as a";
                // Apply Sorting
                string sortColumn = query.SortColumn ?? "UOMSerialID"; // Default column
                string sortDirection = query.SortDirection?.ToUpper() == "DESC" ? "DESC" : "ASC"; // Default to ASC
                sortColumn = SqlHelper.QuoteIdentifier(sortColumn); // Prevent SQL injection
                sql += $" ORDER BY {sortColumn} {sortDirection};";

                var data = _sqlDataAccess.LoadDataQuery<GetUomsWithPaginationDto, dynamic>(sql, parameters).Result.AsEnumerable();
                var filterData = _sortHelper.FilterByName(data.AsQueryable(), query.Filter);
                var result = await filterData.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);
                return await Result<PaginatedResult<GetUomsWithPaginationDto>>.SuccessAsync(result, "Loaded successfully.");

            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<PaginatedResult<GetUomsWithPaginationDto>>.FailureAsync(errors);
            }

        }
    }
}
