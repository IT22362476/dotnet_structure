using AutoMapper;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using INV.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Features.BrandItemType.Command
{
    public class UpdateBrandItemTypeCommand : IRequest<Result<int>>
    {
        [Required]
        public int[]? ItemTypes { get; set; }
        [Required]
        public int BrandSerialID { get; set; }

    }
    internal class UpdateBrandItemTypeCommandHandler : IRequestHandler<UpdateBrandItemTypeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBrandItemTypeRepository _brandItemType;

        public UpdateBrandItemTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IBrandItemTypeRepository brandItemType)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _brandItemType = brandItemType;
        }
        public async Task<Result<int>> Handle(UpdateBrandItemTypeCommand query, CancellationToken cancellationToken)
        {
            UpdateBrandItemTypeValidator validator = new UpdateBrandItemTypeValidator(_brandItemType);
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            // Step 1: Retrieve the brand along with brand item types
            return await _brandItemType.UpdateBrandItemTypesAsync(query, cancellationToken);
        }   
    }
}
