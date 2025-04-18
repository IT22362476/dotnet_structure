using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;

namespace Inv.Application.Features.GRN.Commands
{
   public class ApproveGRNCommand : IRequest<Result<int>>, IMapFrom<Inv.Domain.Entities.GRNHeader>
    {
        [Required]
        public int GRNHeaderSerialID { get; set; }
        [Required]
        public int? ApprovedBy { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApproveGRNCommand, Inv.Domain.Entities.GRNHeader>()
                .ForMember(dest => dest.ApprovedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ApprovedBy, opt => opt.MapFrom(src => src.ApprovedBy));

        }
    }
    internal class ApproveGRNCommandHandler : IRequestHandler<ApproveGRNCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGRNRepository _gRNRepository;

        public ApproveGRNCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IGRNRepository grnRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gRNRepository = grnRepository;
        }

        public async Task<Result<int>> Handle(ApproveGRNCommand query, CancellationToken cancellationToken)
        {
            ApproveGRNCommandValidator validator = new ApproveGRNCommandValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            // Step 1: Retrieve the brand along with brand item types
            return await _gRNRepository.ApproveGRNHeaderAsync(query, cancellationToken);
        }
    }
}
