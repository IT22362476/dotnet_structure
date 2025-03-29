using AutoMapper;
using Inv.Application.DTOs.BinLocation;
using Inv.Application.Features.BinLocation.Command;
using Inv.Application.Helper;
using Inv.Application.Interfaces.Repositories;
using Inv.Application.Request;
using Inv.Domain.Entities;
using Inv.Shared;
using Microsoft.EntityFrameworkCore;

namespace Inv.Persistence.Repositories
{
   
    /// <summary>
    /// Represents a repository for managing companies.
    /// </summary>
    public class BinLocationRepository : IBinLocationRepository
    {
        private readonly IGenericRepository<BinLocation> _repositoryBin;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="RackRepository"/> class.
        /// </summary>
        /// <param name="repository">The repository for managing user companies.</param>
        /// <param name="mapper">The mapper for converting between entity and DTO objects.</param>
        /// <param name="unitOfWork">The unit of work for managing database transactions.</param>
        public BinLocationRepository(IGenericRepository<BinLocation> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repositoryBin = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> CreateBinLocationAsync(CreateBinLocationCommand createBinLocation, CancellationToken cancellationToken)
        {
            try
            {
                var create = _mapper.Map<BinLocation>(createBinLocation);
                await _unitOfWork.Repository<BinLocation>().AddAsync(create);

                var param = _unitOfWork.Repository<TheNumber>().Entities.Where(p => p.TheNumberName == "BinLocation").FirstOrDefault();
                param.LastNumber = param.LastNumber + 1;
                await _unitOfWork.Repository<TheNumber>().UpdateAsync(param, param.TheNumberSerialID);

                // Save changes again to commit the Rack
                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(data: create.RackSerialID, message: "Saved successfully.");
            }
            catch (Exception ex)
            {
                // Rollback transaction in case of an error
                await _unitOfWork.Rollback();
                var message = ex.InnerException?.Message ?? ex.Message;
                return await Result<int>.FailureAsync(message: ex.Message + ex.InnerException);
            }
        }

        public Task<List<GetBinLocationDto>> GetBinLocationAsync(EntityStatus entityStatus, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsUniqueBinLocationAsync(string BinLocation, CancellationToken cancellationToken)
        {
            // verify if the new Brand in the request is unique in the context
            var result = await _unitOfWork.Repository<BinLocation>().Entities.Where(b => b.BinLctn == BinLocation).FirstOrDefaultAsync<BinLocation>();
            if (result == null)
                return true;
            else return false;
        }
        public async Task<bool> IsUniqueBinNameAsync(string BinName, CancellationToken cancellationToken)
        {
            // verify if the new Brand in the request is unique in the context
            var result = await _unitOfWork.Repository<BinLocation>().Entities.Where(b => b.BinName == BinName).FirstOrDefaultAsync<BinLocation>();
            if (result == null)
                return true;
            else return false;
        }

        public async Task<Result<int>> UpdateBinLocationAsync(UpdateBinLocationCommand updateBinLocation, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the existing bin
                var existingBin = await _unitOfWork.Repository<BinLocation>()
                    .GetByIdAsync(updateBinLocation.BinLctnSerialID);

                if (existingBin == null)
                {
                    return await Result<int>.FailureAsync("Bin not found.");
                }
                // Map updated properties to the existing bin
                _mapper.Map(updateBinLocation, existingBin);
                await _unitOfWork.Repository<BinLocation>().UpdateAsync(existingBin, existingBin.BinLctnSerialID);
                // Save changes again to commit 
                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(data: existingBin.BinLctnSerialID, message: "Modified successfully.");
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
