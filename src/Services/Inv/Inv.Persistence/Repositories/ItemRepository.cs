using AutoMapper;
using Inv.Application.DTOs.Item;
using Inv.Application.Features.Item.Command;
using Inv.Application.Interfaces.Repositories;
using Inv.Application.Request;
using Inv.Domain.Entities;
using Inv.Shared;
using JwtTokenAuthentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;


namespace Inv.Persistence.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IGenericRepository<Item> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagingHubService _messagingHubService;
        private readonly TokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemRepository"/> class.
        /// </summary>
        /// <param name="repository">The repository for managing items.</param>
        /// <param name="mapper">The mapper for converting between entity and DTO objects.</param>
        /// <param name="unitOfWork">The unit of work for managing database transactions.</param>
        public ItemRepository(IGenericRepository<Item> repository, IMapper mapper, IUnitOfWork unitOfWork, IMessagingHubService messagingHubService, TokenService tokenService)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _messagingHubService = messagingHubService;
            _tokenService = tokenService;

        }
        public async Task<Result<int>> CreateItemAsync(CreateItemCommand itemCommand, CancellationToken cancellationToken)
        {

            try
            {
                var createItem = _mapper.Map<Item>(itemCommand);
                createItem.ItemCode = $"{createItem.ItemTypeSerialID}-{createItem.ItemSerialID.ToString("D7")}";

                await _unitOfWork.Repository<Item>().AddAsync(createItem);
                // Save changes again to commit the BrandItemTypes
                await _unitOfWork.Save(cancellationToken);
                // Generate the ItemCode using the generated ItemSerialID
                createItem.ItemCode = $"{createItem.ItemTypeSerialID}-{createItem.ItemSerialID:D7}";

                // Update the item with the generated ItemCode
                await _unitOfWork.Repository<Item>().UpdateAsync(createItem, createItem.ItemSerialID);
                await _unitOfWork.Save(cancellationToken);

                // Publish the item created event
                int userSerialID = _tokenService.GetUserSerialID();


                var request = new CreateUserMessageRequest
                {
                    SenderUserSerialID = userSerialID,
                    ReceiverUserSerialIDs = itemCommand.ReceiverUserSerialIDs,
                    MessageText = "approve the item code " + createItem.ItemCode + ".",
                    UserMsgGrpSerialIDs = itemCommand.UserMsgGrpSerialIDs,
                    MessageTypeId = 1
                };
                await _messagingHubService.SendMessageAsync(request);

                return await Result<int>.SuccessAsync(data: createItem.ItemSerialID, message: "Saved successfully.");
            }
            catch (Exception ex)
            {
                // Rollback transaction in case of an error
                await _unitOfWork.Rollback();
                var message = ex.InnerException?.Message ?? ex.Message;
                return await Result<int>.FailureAsync(message: ex.Message + ex.InnerException);
            }
        }

        public Task<List<GetItemDto>> GetItemsAsync(EntityStatus entityStatus, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> IsUniqueItemAsync(string itemCode, CancellationToken cancellationToken)
        {
            // verify if the new Item in the request is unique in the context
            var result = await _unitOfWork.Repository<Item>().Entities.Where(b => b.ItemCode == itemCode).FirstOrDefaultAsync<Item>();
            if (result == null)
                return true;
            else return false;
        }
        public async Task<Result<int>> UpdateItemAsync(UpdateItemCommand updateItem, CancellationToken cancellationToken)
        {

            try
            {
                // Fetch the existing brand and its related item types
                var existingItem = await _unitOfWork.Repository<Item>()
                    .GetByIdAsync(updateItem.ItemSerialID);

                if (existingItem == null)
                {
                    return await Result<int>.FailureAsync("Item not found.");
                }
                // Map updated properties to the existing item
                _mapper.Map(updateItem, existingItem);
                await _unitOfWork.Repository<Item>().UpdateAsync(existingItem, existingItem.ItemSerialID);

                var param = _unitOfWork.Repository<TheNumber>().Entities.Where(p => p.TheNumberName == "Item").FirstOrDefault();
                param.LastNumber = param.LastNumber + 1;
                await _unitOfWork.Repository<TheNumber>().UpdateAsync(param, param.TheNumberSerialID);

                // Save changes again to commit the BrandItemTypes
                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(data: existingItem.ItemSerialID, message: "Modified successfully.");
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
