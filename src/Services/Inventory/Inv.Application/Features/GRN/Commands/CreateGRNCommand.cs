using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.DTOs.GRN;
using Inv.Application.Interfaces.Repositories;
using Inv.Domain.Entities;
using Inv.Shared;
using MediatR;

namespace Inv.Application.Features.GRN.Commands
{
    public class CreateGRNCommand : IRequest<Result<int>> , IMapFrom<GRNHeader>
    {
        [Required]
        public int CompSerialID { get; set; }

        [Required]
        public int SupplierSerialID { get; set; }

        [Required]
        public int SupplierInvoiceNumber { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        [Required]
        public int StoreSerialID { get; set; }

        [Required]
        public List<CreateGRNDetailDto> GRNDetails { get; set; } = new List<CreateGRNDetailDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateGRNCommand, GRNHeader>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.GRNDetails, opt => opt.Ignore());
        }
    }

    internal class CreateGRNCommandHandler : IRequestHandler<CreateGRNCommand, Result<int>>
    {
        private readonly IGRNRepository _grnRepository;

        public CreateGRNCommandHandler(IGRNRepository grnRepository)
        {
            _grnRepository = grnRepository;
        }

        public async Task<Result<int>> Handle(CreateGRNCommand request, CancellationToken cancellationToken)
        {
            // Validate the request using FluentValidation
            CreateGRNCommandValidator validator = new CreateGRNCommandValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<int>.Failure(errors);
            }

            return await _grnRepository.CreateGRNAsync(request, cancellationToken);
        }
    }
}
