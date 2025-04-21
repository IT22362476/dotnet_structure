using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;

namespace Inv.Application.Features.GRN.Commands
{
   public class ApproveGRNDetailCommand : IRequest<Result<int>>, IMapFrom<Inv.Domain.Entities.GRNHeader>
    {
        [Required]
        public int GRNHeaderSerialID { get; set; }
        [Required]
        public int? ApprovedBy { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApproveGRNDetailCommand, Inv.Domain.Entities.GRNHeader>()
                .ForMember(dest => dest.ApprovedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ApprovedBy, opt => opt.MapFrom(src => src.ApprovedBy));

        }
    }
    internal class ApproveGRNCommandHandler : IRequestHandler<ApproveGRNDetailCommand, Result<int>>
    {
        
        private readonly IGRNRepository _gRNRepository;

        public ApproveGRNCommandHandler(IGRNRepository grnRepository)
        {
            _gRNRepository = grnRepository;
        }

        public async Task<Result<int>> Handle(ApproveGRNDetailCommand query, CancellationToken cancellationToken)
        {
            // Valiadte the query using FluentValidation
            ApproveGRNCommandValidator validator = new ApproveGRNCommandValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            
            return await _gRNRepository.ApproveGRNHeaderAsync(query, cancellationToken);
        }
    }
}
