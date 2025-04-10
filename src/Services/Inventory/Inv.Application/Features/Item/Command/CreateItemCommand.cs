using AutoMapper;
using Inv.Application.Features.BrandItemType.Command;
using Inv.Application.Interfaces.Repositories;
using INV.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inv.Application.Features.Item.Command
{
     public class CreateItemCommand : IRequest<Result<int>>
    {
        [Required]
        public int? ItemTypeSerialID { get; set; }
        [Required]
        public int? MainCategorySerialID { get; set; }
        [Required]
        public int? SubCategorySerialID { get; set; }
        [Required]
        public int? ModelSerialID { get; set; }
        [Required]
        public int? BrandSerialID { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? ItemPartNo { get; set; }
        public string? Article { get; set; }
        public string? Remarks { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string? Guage { get; set; }
        public string? Construction { get; set; }
        public string? SpecialFeatures { get; set; }
        [Required]
        public int? UOMSerialID { get; set; }
        public List<int>? UserMsgGrpSerialIDs { get; set; }
        public List<int>? ReceiverUserSerialIDs { get; set; }

    }
    internal class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IItemRepository _item;
        public CreateItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IItemRepository item)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _item = item;
        }

        public async Task<Result<int>> Handle(CreateItemCommand query, CancellationToken cancellationToken)
        {
            CreateItemValidator validator = new CreateItemValidator(_item);
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            // Step 1: Retrieve the brand along with brand item types
            return await _item.CreateItemAsync(query, cancellationToken);
        }
    }
}
