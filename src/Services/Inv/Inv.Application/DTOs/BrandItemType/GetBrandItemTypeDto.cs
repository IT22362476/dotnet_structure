using AutoMapper;
using Inv.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Inv.Application.DTOs.BrandItemType
{
    public class GetBrandItemTypeDto : IMapFrom<Inv.Domain.Entities.BrandItemType>
    {
        [Required]
        public int BITSerialID { get; set; }

        [Required]
        public int BrandSerialID { get; set; }

        [Required]
        public int ItemTypeSerialID { get; set; }

        [Required]
        public string? BrandName { get; set; }

        [Required]
        public string? ItemTypeName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.BrandItemType, GetBrandItemTypeDto>()
                    .ForMember(d => d.BrandName, opt => opt.MapFrom(s => s.Brand.BrandName))
                    .ForMember(d => d.ItemTypeName, opt => opt.MapFrom(s => s.ItemType.ItemTypeName))
                ;

         }
    }

}
