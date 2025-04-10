using AutoMapper;
using Inv.Application.DTOs.Brand;
using Inv.Application.DTOs.BrandItemType;
using Inv.Application.Features.BrandItemType.Command;
using Inv.Application.Interfaces.Repositories;
using Inv.Application.Request;
using Inv.Domain.Entities;
using Inv.Shared;
using INV.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inv.Persistence.Repositories
{
    /// <summary>
    /// Represents a repository for managing companies.
    /// </summary>
    public class BrandItemTypeRepository : IBrandItemTypeRepository
    {
        private readonly IGenericRepository<Brand> _repositoryBrand;
        private readonly IGenericRepository<BrandItemType> _repositoryBIT;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandItemTypeRepository"/> class.
        /// </summary>
        /// <param name="repository">The repository for managing user companies.</param>
        /// <param name="mapper">The mapper for converting between entity and DTO objects.</param>
        /// <param name="unitOfWork">The unit of work for managing database transactions.</param>
        public BrandItemTypeRepository(IGenericRepository<Brand> repository, IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<BrandItemType> repositoryBIT)
        {
            _repositoryBrand = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repositoryBIT = repositoryBIT;
        }
        /// <summary>
        /// Creates brand item types based on the provided DTO.
        /// </summary>
        /// <param name="brandItemTypeDto">The DTO representing the brand and its item types.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of GetBrandItemTypeDto representing the created brand and item types.</returns>
        public async Task<Result<int>> CreateBrandItemTypesAsync(CreateBrandItemTypeCommand brandItemTypeDto, CancellationToken cancellationToken)
        {

            try
           {
                // Fetch the existing brand and its related item types
                var existingBrand = await _unitOfWork.Repository<Brand>()
                    .GetByIdAsync(brandItemTypeDto.BrandSerialID);

                if (existingBrand == null)
                {
                    return await Result<int>.FailureAsync("Brand not found.");
                }
                // Set up the related BrandItemTypes after generating BrandSerialID
                // After saving the Brand, set up the related BrandItemTypes
                // Fetch existing BrandItemTypes for this brand
          
                var existingItemTypeIds = (await _unitOfWork.Repository<BrandItemType>()
                                                .GetAllAsync(b => b.BrandSerialID == brandItemTypeDto.BrandSerialID))
                                                .Select(b => b.ItemTypeSerialID)
                                                .ToHashSet();

                var newItemTypes = brandItemTypeDto.ItemTypes
                    .Where(itemType => !existingItemTypeIds.Contains(itemType))
                    .Select(itemType => new BrandItemType
                    {
                        BrandSerialID = brandItemTypeDto.BrandSerialID,
                        ItemTypeSerialID = itemType,
                        Active = true
                    })
                    .ToList();

                if (newItemTypes.Count > 0)
                {
                    await _unitOfWork.Repository<BrandItemType>().AddRangeAsync(newItemTypes);
                    // Save changes again to commit the BrandItemTypes
                    await _unitOfWork.Save(cancellationToken); 
                }
                    return await Result<int>.SuccessAsync(data: brandItemTypeDto.BrandSerialID, message: "Saved successfully.");
            }
            catch (Exception ex)
            {
                // Rollback transaction in case of an error
                await _unitOfWork.Rollback();
                var message = ex.InnerException?.Message ?? ex.Message;
                return await Result<int>.FailureAsync(message: ex.Message + ex.InnerException);
            }

        }
        /// <summary>
        /// Updates brand item types based on the provided DTO.
        /// </summary>
        /// <param name="brandItemTypeDto">The DTO representing the brand and its item types.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A result with the status of the operation.</returns>
        public async Task<Result<int>> UpdateBrandItemTypesAsync(UpdateBrandItemTypeCommand brandItemTypeDto, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the existing brand and its related item types
                var existingBrand = await _unitOfWork.Repository<Brand>()
                    .GetByIdAsync(brandItemTypeDto.BrandSerialID);

                if (existingBrand == null)
                {
                    return await Result<int>.FailureAsync("Brand not found.");
                }
                // Fetch existing BrandItemTypes for this brand
                var existingBrandItemTypes = await _unitOfWork.Repository<BrandItemType>().GetAllAsync(b => b.BrandSerialID == brandItemTypeDto.BrandSerialID);

                // Prepare a list of new item types to be added
                List<BrandItemType> newItemTypes = new List<BrandItemType>();

                // Update or remove existing BrandItemTypes as needed
                foreach (var brandItemType in existingBrandItemTypes)
                {
                    if (brandItemTypeDto.ItemTypes.Contains(brandItemType.ItemTypeSerialID))
                    {
                        // Update if needed (e.g., if there's a property to update)
                        brandItemType.Active = true; // Assume we need to mark it as active
                        brandItemType.IsDeleted = false;
                        _unitOfWork.Repository<BrandItemType>().UpdateAsync(brandItemType, brandItemType.BITSerialID);
                    }
                    else
                    {

                        // Remove item types that are not in the new DTO
                        // Update if needed (e.g., if there's a property to update)
                        brandItemType.Active = false; // Assume we need to mark it as active
                        brandItemType.IsDeleted = true;
                        _unitOfWork.Repository<BrandItemType>().UpdateAsync(brandItemType, brandItemType.BITSerialID);
                    }
                }

                // Add new BrandItemTypes that are in the DTO but not yet in the database
                foreach (var itemTypeSerialID in brandItemTypeDto.ItemTypes)
                {
                    if (!existingBrandItemTypes.Any(b => b.ItemTypeSerialID == itemTypeSerialID))
                    {
                        newItemTypes.Add(new BrandItemType
                        {
                            BrandSerialID = brandItemTypeDto.BrandSerialID,
                            ItemTypeSerialID = itemTypeSerialID,
                            Active = true
                        });
                    }
                }

                // Add any new BrandItemTypes to the context
                if (newItemTypes.Any())
                {
                    await _unitOfWork.Repository<BrandItemType>().AddRangeAsync(newItemTypes);
                }

                // Save changes to the brand and BrandItemTypes
                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(data: existingBrand.BrandSerialID, message: "Updated successfully.");
            }
            catch (Exception ex)
            {
                // Rollback transaction in case of an error
                await _unitOfWork.Rollback();
                var message = ex.InnerException?.Message ?? ex.Message;
                return await Result<int>.FailureAsync(message: ex.Message + ex.InnerException);
            }
        }


        /// <summary>
        /// Retrieves the list of all companies.
        /// </summary>
        /// <param name="entityStatus">The entity status of the user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of company DTOs.</returns>
        public Task<List<GetBrandItemTypeDto>> GetBrandItemTypesAsync(EntityStatus entityStatus, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsUniqueBrandAsync(string name, CancellationToken cancellationToken)
        {
            // verify if the new Brand in the request is unique in the context
            var result = await _unitOfWork.Repository<Brand>().Entities.Where(b => b.BrandName == name).FirstOrDefaultAsync<Brand>();
            if (result == null)
                return true;
            else return false;
        }
    }
}
