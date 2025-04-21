using AutoMapper;
using Inv.Application.DTOs.DelRecord;
using Inv.Application.DTOs.GRN;
using Inv.Application.Features.GRN.Commands;
using Inv.Application.Features.GRN.Queries;
using Inv.Application.Interfaces.Repositories;
using Inv.Domain.Entities;
using Inv.Persistence.Helper;
using Inv.Shared;
using Microsoft.EntityFrameworkCore;

namespace Inv.Persistence.Repositories
{
    public class GRNRepository : IGRNRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GRNRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GRNHeader?> GetGRNHeaderByIDAsync (GetGRNHeaderByIDQuery query, CancellationToken cancellationToken)
        {
            var grnHeader = await _unitOfWork.Repository<GRNHeader>()
                .Entities
                .Include(gh => gh.GRNDetails)
                    .ThenInclude(gd => gd.Item)
                .Include(gh => gh.GRNDetails)
                    .ThenInclude(gd => gd.UOM)
                .FirstOrDefaultAsync(gh => gh.GRNHeaderSerialID == query.GRNHeaderSerialID, cancellationToken);

            return grnHeader;
        }

        public async Task<Result<int>> CreateGRNAsync(CreateGRNCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var grnHeader = _mapper.Map<GRNHeader>(request);

                    // Update last numbers
                    // Call the UpdateLastNumber method
                    await UpdateNumber.UpdateLastNumber(_unitOfWork, "GRNHeader", grnHeader.CompSerialID);
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    // get the last number
                    var lastNumber = await _unitOfWork.Repository<TheNumber>().Entities
                        .Where(nu => nu.TheNumberName == "GRNHeader" && nu.ComSerialID == grnHeader.CompSerialID)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (lastNumber == null)
                    {
                        throw new Exception("Failed to get a new GRN number.");
                    }

                    grnHeader.GRNID = lastNumber.LastNumber;

                    await _unitOfWork.Repository<GRNHeader>().AddAsync(grnHeader);
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    foreach (var item in request.GRNDetails)
                    {
                        var grnDetail = _mapper.Map<GRNDetail>(item);
                        grnDetail.Active = true;
                        grnHeader.GRNDetails.Add(grnDetail);
                    }

                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);
                    await _unitOfWork.CommitAsync();

                    return Result<int>.Success(grnHeader.GRNHeaderSerialID, message: "GRN created successfully");
                }
                catch (Exception ex)
                {
                    // Rollback transaction in case of an error
                    await _unitOfWork.RollbackAsync();
                    var message = ex.InnerException?.Message ?? ex.Message;
                    return Result<int>.Failure(message: ex.Message + ex.InnerException);
                }
            }
           
        }

        public async Task<Result<int>> DeleteGRNAsync(DeleteGRNCommand delete, CancellationToken cancellationToken)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    // Fetch the existing grn header by its ID
                    var existingGRNHeader = await _unitOfWork.Repository<GRNHeader>()
                        .GetByIdAsync(delete.GRNHeaderSerialID);

                    if (existingGRNHeader == null)
                    {
                        return await Result<int>.FailureAsync("GRN Header not found.");
                    }
                    // Check if the existing grn has already been deleted
                    if (existingGRNHeader.DeletedBy > 0)
                    {
                        return await Result<int>.FailureAsync("Already deleted.");
                    }

                    // Check if the existing grn has already been approved
                    if (existingGRNHeader.ApprovedBy > 0)
                    {
                        return await Result<int>.FailureAsync("Already approved.");
                    }

                    //var entityWithActiveCount = await GetActiveApprovedGRNHeader(existingGRNHeader.GRNHeaderSerialID);
                    // If the existing grn header is active, deactivate it
                    //if (existingGRNHeader.Active )
                    //{
                    //    existingGRNHeader.Active = false;
                    //    existingGRNHeader.IsDeleted = true;

                    //    // Map the updated properties from the update model to the existing entity
                    //    _mapper.Map(delete, existingGRNHeader);

                    //    await _unitOfWork.Repository<GRNHeader>()
                    //        .UpdateAsync(existingGRNHeader, existingGRNHeader.GRNHeaderSerialID);

                    //    // Save the changes to the database
                    //    await _unitOfWork.Save(cancellationToken);
                    //}

                    existingGRNHeader.IsDeleted = true;

                    // Map the updated properties from the update model to the existing entity
                    _mapper.Map(delete, existingGRNHeader);

                    await _unitOfWork.Repository<GRNHeader>()
                        .UpdateAsync(existingGRNHeader, existingGRNHeader.GRNHeaderSerialID);

                    // Save the changes to the database
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    // Create a new del history record for the grn
                    var createDel = new CreateDelRecordDto
                    {
                        DocSerialID = delete.GRNHeaderSerialID,
                        DocTable = delete.DocTable,
                        Remarks = delete.Remarks

                    };
                    var create = _mapper.Map<DelRecord>(createDel);
                    await _unitOfWork.Repository<DelRecord>().AddAsync(create);
                    
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);
                    await _unitOfWork.CommitAsync();

                    return await Result<int>.SuccessAsync(data: existingGRNHeader.GRNHeaderSerialID, message: "Deleted successfully.");
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    await _unitOfWork.Rollback();
                    var message = ex.InnerException?.Message ?? ex.Message;
                    return await Result<int>.FailureAsync(message: message);
                }
            }             
        }

        private async Task<GRNHeader?> GetActiveApprovedGRNHeader(int grnHeaderSerialID)
        {
            return await _unitOfWork.Repository<GRNHeader>().Entities
                .Where(g => g.GRNHeaderSerialID == grnHeaderSerialID && !g.IsDeleted && g.ApprovedDate != null)
                .OrderByDescending(x => x.ApprovedDate)
                .FirstOrDefaultAsync();
        }

        public async Task<Result<int>> DeleteGRNDetailAsync(DeleteGRNDetailCommand delete, CancellationToken cancellationToken)
        {
            // Fetch the existing grn detail by its ID
            var existingGRNDetail = await _unitOfWork.Repository<GRNDetail>()
                .GetByIdAsync(delete.GRNDetailSerialID);

            if (existingGRNDetail == null)
            {
                return await Result<int>.FailureAsync("GRN Detail not found.");
            }

            // Check if the existing grn detail has already been deleted
            if (existingGRNDetail.IsDeleted)
            {
                return await Result<int>.FailureAsync("Already deleted.");
            }

            existingGRNDetail.IsDeleted = true;

            await _unitOfWork.Repository<GRNDetail>()
                        .UpdateAsync(existingGRNDetail, existingGRNDetail.GRNDetailSerialID);

            // Save the changes to the database
            await _unitOfWork.Save(cancellationToken);

            return await Result<int>.SuccessAsync(data: existingGRNDetail.GRNDetailSerialID, message: "Deleted successfully.");
        }

        public async Task<Result<int>> ApproveGRNHeaderAsync(ApproveGRNDetailCommand approve, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the existing grnheader by its id
                var existingGRNHeader = await _unitOfWork.Repository<GRNHeader>()
                    .GetByIdAsync(approve.GRNHeaderSerialID);

                if (existingGRNHeader == null)
                {
                    return await Result<int>.FailureAsync("GRN Header not found.");
                }
                if (existingGRNHeader.ApprovedBy > 0)
                {
                    return await Result<int>.FailureAsync("Already approved.");
                }
                // Map updated properties to the existing external asset location
                existingGRNHeader.Active = true; // active the existing external asset location for the new location,update rental asset register ExAssetLocSerialID
                _mapper.Map(approve, existingGRNHeader);
                await _unitOfWork.Repository<GRNHeader>().UpdateAsync(existingGRNHeader, existingGRNHeader.GRNHeaderSerialID);
                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(data: existingGRNHeader.GRNHeaderSerialID, message: "Approved successfully.");
            }
            catch (Exception ex)
            {
                // Rollback transaction in case of an error
                await _unitOfWork.Rollback();
                var message = ex.InnerException?.Message ?? ex.Message;
                return await Result<int>.FailureAsync(message: ex.Message + ex.InnerException);
            }
        }

    }
}
