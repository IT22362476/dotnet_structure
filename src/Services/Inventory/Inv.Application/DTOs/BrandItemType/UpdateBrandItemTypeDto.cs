using AutoMapper;
using Inv.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Inv.Application.DTOs.BrandItemType
{
    public class UpdateBrandItemTypeDto : IMapFrom<Inv.Domain.Entities.BrandItemType>
    {
        [Required]
        public int BITSerialID { get; set; }
        [Required]
        public int BrandSerialID { get; set; }
        [Required]
        public int ItemTypeSerialID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBrandItemTypeDto, Inv.Domain.Entities.BrandItemType>();
        }
    }
}
