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
        public string? SupplierInvoiceNumber { get; set; }

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
        private readonly ISystemPORepository _systemPORepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGRNCommandHandler(IGRNRepository grnRepository, IUnitOfWork unnitOfWork, ISystemPORepository systemPORepository)
        {
            _grnRepository = grnRepository;
            _unitOfWork = unnitOfWork;
            _systemPORepository = systemPORepository;
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

            // Check if the GRN Details are not null
            if (request.GRNDetails == null || request.GRNDetails.Count == 0)
            {
                return Result<int>.Failure("GRN Details are required.");
            }

            // collect all po numbers in grn details
            var poNumbers = request.GRNDetails
                .Select(x => x.SystemPOSerialID)
                .Distinct()
                .ToList();

            // fetch all related POs
            var relatedPOs = await _systemPORepository.GetPOsWithDetails(poNumbers, cancellationToken);

            // lookup dictionay for faster access
            var poHeaderLookup = relatedPOs.ToDictionary(p => p.SystemPOHeaderSerialID);
            var poDetailsLookup = relatedPOs
                .SelectMany(p => p.SystemPODetails)
                .ToDictionary(d => (d.SystemPOHeaderSerialID, d.ItemSerialID));

            // GRN validation
            var grnValidationErrors = new List<string>();

            foreach( var grnItem in request.GRNDetails)
            {
                // check if the po number exists
                if (!poHeaderLookup.ContainsKey(grnItem.SystemPOSerialID))
                {
                    grnValidationErrors.Add($"PO Number {grnItem.SystemPOSerialID} not found.");
                    continue;
                }

                // check if item exists in po
                var key = (grnItem.SystemPOSerialID, grnItem.ItemSerialID);
                if(!poDetailsLookup.TryGetValue(key, out var poDetail))
                {
                    grnValidationErrors.Add($"Item {grnItem.ItemSerialID} not found in PO {grnItem.SystemPOSerialID}");
                    continue;
                }

                // quantity validation
                if(grnItem.Qty > poDetail.BalToReceive)
                {
                    grnValidationErrors.Add($"Item {grnItem.ItemSerialID} quantity {grnItem.Qty} exceeds the balance to receive quantity in PO {grnItem.SystemPOSerialID}.");
                    continue;
                }
            }

            if(grnValidationErrors.Any())
            {
                return Result<int>.Failure(grnValidationErrors);
            }

            return await _grnRepository.CreateGRNAsync(request, cancellationToken);
        }
    }
}
