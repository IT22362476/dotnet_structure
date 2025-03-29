using AutoMapper;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using INV.Application.Interfaces.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Inv.Application.Features.BrandItemType.Command
{
    public class CreateBrandItemTypeCommand : IRequest<Result<int>>
    {
        [Required]
        public int[]? ItemTypes { get; set; }
        [Required]
        public int BrandSerialID { get; set; }
    }
    internal class CreateBrandItemTypeCommandHandler : IRequestHandler<CreateBrandItemTypeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBrandItemTypeRepository _brandItemType;

        public CreateBrandItemTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IBrandItemTypeRepository brandItemType)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _brandItemType = brandItemType;
        }

        public async Task<Result<int>> Handle(CreateBrandItemTypeCommand query, CancellationToken cancellationToken)
        {
            CreateBrandItemTypeValidator validator = new CreateBrandItemTypeValidator(_brandItemType);
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            // Step 1: Retrieve the brand along with brand item types
            return  await _brandItemType.CreateBrandItemTypesAsync(query, cancellationToken);
        }
    }
}
