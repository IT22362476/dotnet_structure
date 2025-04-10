using Inv.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INV.Application.Interfaces.Repositories;

namespace Inv.Application.Features.BrandItemType.Command
{
    public class UpdateBrandItemTypeValidator : AbstractValidator<UpdateBrandItemTypeCommand>
    {
        private readonly IBrandItemTypeRepository _brandItemType;

        public UpdateBrandItemTypeValidator(IBrandItemTypeRepository brandItemType)
        {
            _brandItemType = brandItemType;

            RuleFor(x => x.ItemTypes)
                    .NotEmpty()
                    .NotNull()
                    .Must(itemTypes => itemTypes != null && itemTypes.Length>0)
                    .WithMessage("At least one item type is required.");
            RuleFor(x => x.BrandSerialID)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("The brand is required.");
        }
        
    }
}
