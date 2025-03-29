using AutoMapper;
using Inv.Application.Features.BrandItemType.Command;
using Inv.Application.Interfaces.Repositories;
using INV.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Inv.Application.Common.Mappings;

namespace Inv.Application.Features.Rack.Command
{
    public class UpdateRackCommand : IRequest<Result<int>>,IMapFrom<Domain.Entities.Rack>
    {
        [Required]
        public int RackSerialID { get; set; }

        [Required]
        [Display(Name = "Rack Name")]
        [StringLength(30, ErrorMessage = "The rack name cannot exceed 30 characters. ")]
        public string? RackName { get; set; }

        [Required]
        public int Rows { get; set; }

        [Required]
        public int Columns { get; set; }

        public int? Compartments { get; set; }

        [Required]
        public int ComSerialID { get; set; }

        [Required]
        public int WHSerialID { get; set; }
         
        [Required]
        public int StoreSerialID { get; set; }

        [Required]
        public int ZoneSerialID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateRackCommand, Domain.Entities.Rack>();
        }

    }
    internal class UpdateRackCommandHandler : IRequestHandler<UpdateRackCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRackRepository _rack;
   

        public UpdateRackCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IRackRepository rack)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rack = rack;
        }
        public async Task<Result<int>> Handle(UpdateRackCommand query, CancellationToken cancellationToken)
        {
            UpdateRackValidator validator = new UpdateRackValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            // Step 1: Retrieve the brand along with brand item types
            return await _rack.UpdateRackAsync(query, cancellationToken);
        }
    }
}
