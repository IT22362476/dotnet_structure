using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.DTOs.Rack;
using Inv.Application.Features.Item.Command;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.Rack.Command
{
    public class CreateRackCommand : IRequest<Result<int>>,IMapFrom<CreateRackCommand>
    {
        [Required]
        [Display(Name = "Rack Name")]
        [StringLength(30, ErrorMessage = "The rack name cannot exceed 30 characters. ")]
        public string? RackName { get; set; }

        [Required]
        [Display(Name = "Rack Code")]
        [StringLength(10, ErrorMessage = "The rack code cannot exceed 10 characters. ")]
        public string? RackCode { get; set; }

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
            profile.CreateMap<CreateRackCommand, Inv.Domain.Entities.Rack>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }


    }
    internal class CreateRackCommandHandler : IRequestHandler<CreateRackCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRackRepository _rack;

        public CreateRackCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IRackRepository rack)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rack = rack;
        }

        public async Task<Result<int>> Handle(CreateRackCommand query, CancellationToken cancellationToken)
        {
            CreateRackValidator validator = new CreateRackValidator(_rack);
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            return await _rack.CreateRackAsync(query, cancellationToken);
        }
    }
}
