using AutoMapper;
using Inv.Application.Features.GRN.Commands;
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
        /*
        public async Task<Result<int>> CreateGRNAsync(CreateGRNCommand request, CancellationToken cancellationToken)
        {
            // Check if the GRN Details are not null
            if (request.GRNDetails == null || request.GRNDetails.Count == 0)
            {
                return Result<int>.Failure("GRN Details are required.");
            }

            // check for correct bin location**


            foreach (var grnDetail in request.GRNDetails)
            {
                // Check if the PO of the grn item exists
                //var purchaseOrder = await _unitOfWork.Repository<PurchaseOrder>().GetEntityWithIncludesAsync(
                //    po => po.POSerialID == grnDetail.SystemPOSerialID,
                //    po => po.PurchaseOrderItem);

                var purchaseOrder = await _unitOfWork.Repository<PurchaseOrder>().Entities
                    .Include(po => po.PurchaseOrderItem)
                    .Where(po => po.POSerialID == grnDetail.SystemPOSerialID)
                    .FirstOrDefaultAsync();

                if(purchaseOrder is null)
                {
                    return Result<int>.Failure($"Purchase order not found for the item {grnDetail.ItemSerialID}.");
                }

                if(purchaseOrder.PurchaseOrderItem == null || purchaseOrder.PurchaseOrderItem.Count == 0)
                {
                    return Result<int>.Failure($"Purchase Details not found in Purchase Order, {grnDetail.SystemPOSerialID}.");
                }

                // Check if the grn details exists in the purchase order
                var purchaseOrderDetail = purchaseOrder.PurchaseOrderItem.FirstOrDefault(x => x.ItemSerialID == grnDetail.ItemSerialID);
                if (purchaseOrderDetail == null)
                {
                    return Result<int>.Failure($"Item not found in Purchse Order Serial ID {grnDetail.SystemPOSerialID}.");
                }
                else
                {
                    // Check if the quantity is greater than the ordered quantity from PO
                    if (grnDetail.Qty > purchaseOrderDetail.Quantity)
                    {
                        return Result<int>.Failure($" Quantity of item {grnDetail.ItemSerialID} exceeds available quantity.");
                    }
                }

                // ********* BatchBalQty and AssetCount *****************
            }

            var grn = _mapper.Map<GRNHeader>(request);
            if (grn is null)
            {
                return Result<int>.Failure("Failed to map the request to GRN.");
            }
            
            // Add the GRN to the repository
            try
            {
                // Begin transaction (it will reuse an existing one if already started)
                using (var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken))
                {
                    // Update last numbers
                    // Call the UpdateLastNumber method
                    await UpdateNumber.UpdateLastNumber(_unitOfWork, "GRNHeader", grn.CompSerialID);
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    // get the last number
                    var lastNumber = await _unitOfWork.Repository<TheNumber>().Entities
                        .Where(nu => nu.TheNumberName == "GRNHeader" && nu.ComSerialID == grn.CompSerialID)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (lastNumber == null)
                    {
                        throw new Exception("Last number not found.");
                    }

                    grn.GRNID = lastNumber.LastNumber;

                    await _unitOfWork.Repository<GRNHeader>().AddAsync(grn);
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    // Add the GRN items to the GRN entity

                    foreach (var item in request.GRNDetails)
                    {
                        var grnLineItem = _mapper.Map<GRNDetail>(item);
                        grnLineItem.GRNHeaderSerialID = grn.GRNHeaderSerialID;
                        grn.GRNDetails.Add(grnLineItem);
                    }

                    // Save the updated GRN
                    //await _unitOfWork.Repository<GRNHeader>().UpdateAsync(grn, grn.GRNHeaderSerialID);
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    // Deduct GRN quantity from the purchase order
                    //foreach (var lineItem in request.GRNItems)
                    //{
                    //    var purchaseOrderLineItem = purchaseOrder.PurchaseOrderItem.FirstOrDefault(x => x.POItemSerialID == lineItem.POItemSerialID);
                    //    if (purchaseOrderLineItem != null)
                    //    {
                    //        purchaseOrderLineItem.Quantity -= lineItem.ReceivedQty;

                    //    }

                    //}

                    //Check if th purchse order is completed
                    //if (purchaseOrder.PurchaseOrderItem.All(x => x.Quantity == 0))
                    //{
                    //    // Mark the purchase order as completed****************** Need status prop for PO
                    //}

                    // Save the updated purchase order
                    //await _unitOfWork.Repository<PurchaseOrder>().UpdateAsync(purchaseOrder, purchaseOrder.POSerialID);
                    //await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    // Commit the transaction
                    await _unitOfWork.CommitAsync();

                    return Result<int>.Success(grn.GRNHeaderSerialID, message: "GRN created successfully");
                }

            }
            catch (Exception ex)
            {
                // Rollback transaction in case of an error
                await _unitOfWork.RollbackAsync();
                var message = ex.InnerException?.Message ?? ex.Message;
                return Result<int>.Failure(message: ex.Message + ex.InnerException);
            }
        }*/

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
    }
}
