using AutoMapper;
using Inv.Application.Interfaces.Repositories;
using Inv.Shared;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Inv.Application.Features.Item.Command
{
    public class UpdateItemCommand : IRequest<Result<int>>
    {
        [Required]
        public int ItemSerialID { get; set; }
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
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
        internal class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IItemRepository _item;

        public UpdateItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IItemRepository item)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _item = item;
        }

        public async Task<Result<int>> Handle(UpdateItemCommand query, CancellationToken cancellationToken)
        {
            UpdateItemValidation validator = new UpdateItemValidation(_item);
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            // Step 1: Retrieve the brand along with brand item types
            return await _item.UpdateItemAsync(query, cancellationToken);
        }
    }

}
