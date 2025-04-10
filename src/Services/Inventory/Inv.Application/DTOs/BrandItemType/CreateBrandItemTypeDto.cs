using AutoMapper;
using Inv.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Inv.Application.DTOs.BrandItemType
{
    public class CreateBrandItemTypeDto : IMapFrom<Inv.Domain.Entities.BrandItemType>
    {
        [Required]
        public int[]? ItemTypes { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? BrandName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBrandItemTypeDto, Inv.Domain.Entities.BrandItemType>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
