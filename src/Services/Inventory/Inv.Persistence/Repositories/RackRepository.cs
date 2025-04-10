using AutoMapper;
using Inv.Application.Interfaces.Repositories;
using Inv.Domain.Entities;
using Inv.Shared;
using Inv.Application.Features.Rack.Command;
using Inv.Application.DTOs.Rack;
using Inv.Application.Request;
using Microsoft.EntityFrameworkCore;
using Inv.Application.Helper;

namespace Inv.Persistence.Repositories
{
     /// <summary>
    /// Represents a repository for managing companies.
    /// </summary>
    public class RackRepository : IRackRepository
    {
        private readonly IGenericRepository<Rack> _repositoryRack;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="RackRepository"/> class.
        /// </summary>
        /// <param name="repository">The repository for managing user companies.</param>
        /// <param name="mapper">The mapper for converting between entity and DTO objects.</param>
        /// <param name="unitOfWork">The unit of work for managing database transactions.</param>
        public RackRepository(IGenericRepository<Rack> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repositoryRack = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> CreateRackAsync(CreateRackCommand createRack, CancellationToken cancellationToken)
        {
            try
            {
                var create = _mapper.Map<Rack>(createRack); 

                await _unitOfWork.Repository<Rack>().AddAsync(create);

                var param = _unitOfWork.Repository<TheNumber>().Entities.Where(p => p.TheNumberName == "Rack").FirstOrDefault();
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

        public Task<List<GetRackDto>> GetRackAsync(EntityStatus entityStatus, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsUniqueRackCodeAsync(string rackCode, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Repository<Rack>().Entities.Where(b => b.RackCode == rackCode).FirstOrDefaultAsync<Rack>();
            if (result == null)
                return true;
            else return false;
        }

        public async Task<Result<int>> UpdateRackAsync(UpdateRackCommand updateRack, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the existing rack
                var existingRack = await _unitOfWork.Repository<Rack>()
                    .GetByIdAsync(updateRack.RackSerialID);

                if (existingRack == null)
                {
                    return await Result<int>.FailureAsync("Item not found.");
                }
                // Map updated properties to the existing item
                _mapper.Map(updateRack, existingRack);
                await _unitOfWork.Repository<Rack>().UpdateAsync(existingRack, existingRack.RackSerialID);
                // Save changes again to commit 
                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(data: existingRack.RackSerialID, message: "Modified successfully.");
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
