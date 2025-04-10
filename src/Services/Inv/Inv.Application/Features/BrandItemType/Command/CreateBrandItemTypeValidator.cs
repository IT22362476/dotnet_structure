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
    public class CreateBrandItemTypeValidator : AbstractValidator<CreateBrandItemTypeCommand>
    {
        private readonly IBrandItemTypeRepository _brandItemType;

        public CreateBrandItemTypeValidator(IBrandItemTypeRepository brandItemType)
        {
            _brandItemType = brandItemType;

            RuleFor(x => x.BrandSerialID)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("The brand is required.");
                    // .MustAsync(IsUniqueBrandAsync)
                   // .WithMessage("The brand is unique");
            RuleFor(x => x.ItemTypes)
                    .Must(itemTypes => itemTypes != null && itemTypes.Length>0)
                    .WithMessage("At least one item type is required.");

        }
  /*      private async Task<bool> IsUniqueBrandAsync(string barnd, CancellationToken cancellationToken)
        {
            bool isExistingUsername = await _brandItemType.IsUniqueBrandAsync(barnd, cancellationToken);
            return isExistingUsername;
        }*/
    }
}
