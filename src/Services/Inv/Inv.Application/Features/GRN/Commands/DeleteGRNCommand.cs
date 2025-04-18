
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;

namespace Inv.Application.Features.GRN.Commands
{
    public class DeleteGRNCommand : IRequest<Result<int>>, IMapFrom<Inv.Domain.Entities.GRNHeader>
    {
        [Required]
        public int GRNHeaderSerialID { get; set; }
        [Required]
        public int? DeletedBy { get; set; }
        [Required]
        public string? DocTable { get; set; }
        [Required]
        [NotMapped]
        public string? Remarks { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteGRNCommand, Inv.Domain.Entities.GRNHeader>()
                .ForMember(dest => dest.DeletedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.DeletedBy, opt => opt.MapFrom(src => src.DeletedBy));

        }
    }
    internal class DeleteGRNCommandHandler : IRequestHandler<DeleteGRNCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGRNRepository _gRNRepository;

        public DeleteGRNCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IGRNRepository grnRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gRNRepository = grnRepository;
        }

        public async Task<Result<int>> Handle(DeleteGRNCommand query, CancellationToken cancellationToken)
        {
            DeleteGRNCommandValidator validator = new DeleteGRNCommandValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            // Step 1: Retrieve the brand along with brand item types
            return await _gRNRepository.DeleteGRNAsync(query, cancellationToken);
        }
    }
}
