using System.Data;
using Dapper;
using Inv.Application.DTOs.GRN;
using Inv.Application.DTOs.SystemPO;
using Inv.Application.Features.GRN.Queries;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;

namespace Inv.Application.Features.SystemPO.Queries
{
    public class GetOpenPOsByItemQuery : IRequest<Result<List<GetSystemPOByItemDto>>>
    {
        public int ItemSerialID { get; set; }

        public GetOpenPOsByItemQuery(int itemSerialID)
        {
            ItemSerialID = itemSerialID;
        }
    }

    internal class GetOpenPOsByItemQueryHandler : IRequestHandler<GetOpenPOsByItemQuery, Result<List<GetSystemPOByItemDto>>>
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public GetOpenPOsByItemQueryHandler(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<Result<List<GetSystemPOByItemDto>>> Handle(GetOpenPOsByItemQuery query, CancellationToken cancellationToken)
        {
            // Validate the request using FluentValidation
            var validator = new GetOpenPOsByItemQueryValidator();
            var validationResult = validator.Validate(query);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<List<GetSystemPOByItemDto>>.Failure(errors);
            }

            var parameters = new DynamicParameters();
            parameters.Add("@ItemSerialID", query.ItemSerialID, DbType.Int64, ParameterDirection.Input, query.ItemSerialID);

            var sql = @"
                        SELECT 
	                        ph.SystemPOHeaderSerialID,
	                        ph.SystemPOID,
	                        pd.SystemPODetailSerialID,
	                        pd.ItemSerialID,
	                        pd.BalToReceive
                        FROM [Inv].[SystemPOHeader] ph
                        INNER JOIN [Inv].[SystemPODetail] pd ON pd.SystemPOHeaderSerialID = ph.SystemPOHeaderSerialID
                        WHERE 
	                        ph.IsComplete = 0
                            AND ph.IsDeleted = 0
	                        AND pd.ItemSerialID = @ItemSerialID
	                        AND pd.BalToReceive > 0
                        ORDER BY ph.SystemPOID, pd.LineNumber";

            var pos = await _sqlDataAccess.LoadDataQuery<GetSystemPOByItemDto, dynamic>(sql, parameters);
            //if (!pos.Any())
            //{
            //    return Result<List<GetSystemPOByItemDto>>.Failure("No open POs found for the item.");
            //}

            return Result<List<GetSystemPOByItemDto>>.Success(pos.ToList());
        }
    }
}
