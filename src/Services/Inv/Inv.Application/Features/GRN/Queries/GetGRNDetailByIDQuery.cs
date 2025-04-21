using Dapper;
using System.Data;
using Inv.Application.DTOs.GRN;
using Inv.Shared;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Inv.Application.Interfaces.Repositories;
using FluentValidation;

namespace Inv.Application.Features.GRN.Queries
{
    public class GetGRNDetailByIDQuery : IRequest<Result<GetGRNDetailByIDDto>>
    {
        public int GRNDetailSerialID { get; }

        public GetGRNDetailByIDQuery(int grnDetailSerialID)
        {
            GRNDetailSerialID = grnDetailSerialID;
        }

    }

    internal class GetGRNDetailByIDQueryHandler : IRequestHandler<GetGRNDetailByIDQuery, Result<GetGRNDetailByIDDto>>
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public GetGRNDetailByIDQueryHandler(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<Result<GetGRNDetailByIDDto>> Handle(GetGRNDetailByIDQuery query, CancellationToken cancellationToken)
        {
            // Validate the request using FluentValidation
            var validator = new GetGRNDetailByIDQueryValidator();
            var validationResult = validator.Validate(query);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<GetGRNDetailByIDDto>.Failure(errors);
            }

            var parameters = new DynamicParameters();
            parameters.Add("@grnDetailSerialID", query.GRNDetailSerialID, DbType.Int64, ParameterDirection.Input, query.GRNDetailSerialID);

            var sql = @"
                        SELECT 
                            d.GRNDetailSerialID,
                            d.GRNHeaderSerialID,
                            d.LineNumber,
                            d.ItemSerialID,
                            item.ItemCode,
                            d.SystemPOSerialID,
                            d.BatchNumber,
                            d.ExpiryDate,
                            d.WarrentyPeriod,
                            d.UOMSerialID,
                            u.UOMName,
                            d.Condition,
                            d.Qty,
                            d.FOCQty,
                            d.BatchBalQty,
                            d.AssetCount,
                            d.Remarks
                        FROM 
                            [Inv].[GRNDetail] d
                        LEFT JOIN [INVDB].[Inv].UOM AS u ON u.UOMSerialID = d.UOMSerialID
                        LEFT JOIN [INVDB].[Inv].Item AS item ON item.ItemSerialID = d.ItemSerialID
                        WHERE 
                            d.GRNDetailSerialID = @grnDetailSerialID
                            AND d.IsDeleted = 0;";

            var grnDetail = await _sqlDataAccess.SingleDataQuery<GetGRNDetailByIDDto, dynamic>(sql, parameters);
            if(grnDetail == null)
            {
                return Result<GetGRNDetailByIDDto>.Failure("GRN Detail not found");
            }

            return Result<GetGRNDetailByIDDto>.Success(grnDetail, "GRN Detail retrieved successfully");
        }
    }
}
