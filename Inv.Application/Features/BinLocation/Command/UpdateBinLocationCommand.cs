using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.DTOs.Brand;
using Inv.Application.Features.Rack.Command;
using Inv.Application.Interfaces.Repositories;
using Inv.Domain.Entities;
using Inv.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.BinLocation.Command
{
    public class UpdateBinLocationCommand : IRequest<Result<int>>,IMapFrom<Domain.Entities.BinLocation>
    {
        [Required]
        public int BinLctnSerialID { get; set; }
        [Required]
        public string? BinName { get; set; }
        [Required]
        public string? BinLctn { get; set; }
  
        [Required]
        public int ItemSerialID { get; set; }
        [Required]
        public int Row { get; set; }

        [Required]
        public string? Column { get; set; }
        [Required]
        public int? Compartment { get; set; }
        [Required]
        public int RackSerialID { get; set; }

        [Required]
        public int ComSerialID { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        [Required]
        public int StoreSerialID { get; set; }

        [Required]
        public int ZoneSerialID { get; set; }
        [Required]
        public int ItemConditionId { get; set; }
        public bool IsVoidBinLocation { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBinLocationCommand, Inv.Domain.Entities.BinLocation>()
                    .ForMember(dest => dest.ItemCondition, opt => opt.MapFrom(src => (ItemCondition)src.ItemConditionId));
        }

    }
    internal class UpdateBinLocationCommandHandler : IRequestHandler<UpdateBinLocationCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBinLocationRepository _binLocationRepository;


        public UpdateBinLocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IBinLocationRepository binLocationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _binLocationRepository = binLocationRepository;
        }
        public async Task<Result<int>> Handle(UpdateBinLocationCommand query, CancellationToken cancellationToken)
        {
            UpdateBinLocationValidator validator = new UpdateBinLocationValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            // Step 1: Retrieve the brand along with brand item types
            return await _binLocationRepository.UpdateBinLocationAsync(query, cancellationToken);
        }
    }

}
