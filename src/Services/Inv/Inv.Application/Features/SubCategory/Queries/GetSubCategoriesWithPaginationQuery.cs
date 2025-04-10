using AutoMapper;
using Dapper;
using Inv.Application.DTOs.SubCategory;
using Inv.Application.Extensions;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;
using System.Data;


namespace Inv.Application.Features.SubCategory.Queries
{
    public record GetSubCategoriesWithPaginationQuery : IRequest<Result<PaginatedResult<GetSubCategoriesWithPaginationDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }

        public GetSubCategoriesWithPaginationQuery() { }

        public GetSubCategoriesWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetAssetsWithPaginationQueryHandler : IRequestHandler<GetSubCategoriesWithPaginationQuery, Result<PaginatedResult<GetSubCategoriesWithPaginationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetSubCategoriesWithPaginationDto> _sortHelper;


        public GetAssetsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, ISortHelper<GetSubCategoriesWithPaginationDto> sortHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqlDataAccess = sqlDataAccess;
            _sortHelper = sortHelper;
        }

        public async Task<Result<PaginatedResult<GetSubCategoriesWithPaginationDto>>> Handle(GetSubCategoriesWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetSubCategoriesWithPaginationValidator validator = new GetSubCategoriesWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query);

            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
                parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);
                parameters.Add("@filteringCol", query.Filter, DbType.String, ParameterDirection.Input, 200);
                parameters.Add("@SortingCol", query.Filter, DbType.String, ParameterDirection.Input, 200);

                var sql = @"SELECT ROW_NUMBER() OVER(ORDER BY AssetId) AS Id,* FROM [CoreDB].[Core].[Asset] as a";


                var data = _sqlDataAccess.LoadDataQuery<GetSubCategoriesWithPaginationDto, dynamic>(sql, parameters).Result.AsEnumerable();
                var filterData = _sortHelper.FilterByName(data.AsQueryable(), query.Filter);
                var result = await filterData.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);
                return await Result<PaginatedResult<GetSubCategoriesWithPaginationDto>>.SuccessAsync(result, "Loaded successfully.");

            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<PaginatedResult<GetSubCategoriesWithPaginationDto>>.FailureAsync(errors);
            }

        }
    }
}
