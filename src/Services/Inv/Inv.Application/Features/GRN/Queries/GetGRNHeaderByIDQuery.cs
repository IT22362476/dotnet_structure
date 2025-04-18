using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Inv.Application.DTOs.GRN;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Inv.Application.Features.GRN.Queries
{
    public class GetGRNHeaderByIDQuery : IRequest<Result<GetGRNHeaderByIdDto>>
    {
        [Required]
        public int GRNHeaderSerialID { get; set; }

        public GetGRNHeaderByIDQuery(int grnHeaderSerialID)
        {
            GRNHeaderSerialID = grnHeaderSerialID;
        }
    }

    internal class GetGRNHeaderByIDQueryHandler : IRequestHandler<GetGRNHeaderByIDQuery, Result<GetGRNHeaderByIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGRNRepository _grnRepository;

        public GetGRNHeaderByIDQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IGRNRepository grnRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _grnRepository = grnRepository;
        }

        public async Task<Result<GetGRNHeaderByIdDto>> Handle(GetGRNHeaderByIDQuery query, CancellationToken cancellationToken)
        {
            // Validate the request using FluentValidation
            var validator = new GetGRNHeaderByIDQueryValidator();
            var validationResult = validator.Validate(query);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<GetGRNHeaderByIdDto>.Failure(errors);
            }

            var grnHeader = await _grnRepository.GetGRNHeaderByIDAsync(query, cancellationToken);

            return grnHeader != null 
                ? Result<GetGRNHeaderByIdDto>.Success(_mapper.Map<GetGRNHeaderByIdDto>(grnHeader), "GRN retrived successfully")
                : Result<GetGRNHeaderByIdDto>.Failure("GRN not found");
        }
    }
}
