using AutoMapper;
using Inv.Application.Common.Mappings;
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
    public class CreateBinLocationCommand : IRequest<Result<int>>, IMapFrom<CreateBinLocationCommand>
    {
        [Required]
        public string? BinName { get; set; }
        [Required]
        public int ComSerialID { get; set; }
        [Required]
        public int ItemSerialID { get; set; }
        [Required]
        public int WHSerialID { get; set; }
        [Required]
        public int StoreSerialID { get; set; }
        [Required]
        public int ZoneSerialID { get; set; }
        [Required]
        public int RackSerialID { get; set; }
        [Required]
        public string? BinLctn { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        public string? Column { get; set; }
        [Required]
        public int? Compartment { get; set; }
        [Required]
        public int  ItemConditionId { get; set; } 
        public bool IsVoidBinLocation { get; set; } 

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBinLocationCommand, Inv.Domain.Entities.BinLocation>()
                           .ForMember(dest => dest.ItemCondition, opt => opt.MapFrom(src => (ItemCondition)src.ItemConditionId))
                           .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));
        }


    }
    internal class CreateBinLocationCommandHandler : IRequestHandler<CreateBinLocationCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBinLocationRepository _bin;

        public CreateBinLocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IBinLocationRepository bin)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bin = bin;
        }

        public async Task<Result<int>> Handle(CreateBinLocationCommand query, CancellationToken cancellationToken)
        {
            CreateBinLocationValidator validator = new CreateBinLocationValidator(_bin);
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            return await _bin.CreateBinLocationAsync(query, cancellationToken);
        }
    }
}
