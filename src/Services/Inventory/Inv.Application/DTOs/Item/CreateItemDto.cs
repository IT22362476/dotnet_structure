using System.ComponentModel.DataAnnotations;
using Inv.Application.Common.Mappings;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Inv.Application.Features.Item.Command;

namespace Inv.Application.DTOs.Item
{
    public class CreateItemDto : IMapFrom<Inv.Domain.Entities.Item>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateItemCommand, Inv.Domain.Entities.Item>()
                   .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                   .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

        }
    }
}
