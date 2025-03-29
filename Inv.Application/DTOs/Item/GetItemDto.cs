using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.Item
{
    public class GetItemDto : IMapFrom<Inv.Domain.Entities.Item>
    {
        [Required]
        public int ItemSerialID { get; set; }
        [Required]
        public string? ItemCode { get; set; }
        [Required]
        public int? ItemTypeSerialID { get; set; }
        [Required]
        public string? ItemDes { get; set; }
        [Required]
        public int? MainCategorySerialID { get; set; }
        [Required]
        public int? SubCategorySerialID { get; set; }
        [Required]
        public int? ModelSerialID { get; set; }
        [Required]
        public int BrandSerialID { get; set; }
        [Required]
        public string? MainCategoryName { get; set; }
        [Required]
        public string? SubCategoryName { get; set; }
        [Required]
        public string? BrandName { get; set; }
        [Required]
        public string? ModelName { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public double Volume { get; set; }
        [Required]
        public string? Size { get; set; }
        [Required]
        public string? Color { get; set; }
        [Required]
        public string? ItemPartNo { get; set; }

        public string? ItemTypeName { get; set; }

        [Required]
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.Item, GetItemDto>()
                .ForMember(dest => dest.ItemDes, 
                opt => opt.MapFrom(src => 
                src.Brand.BrandName+" "+src.Model.ModelName+" "+src.MainCategory.MainCategoryName+" "+src.SubCategory.SubCategoryName))
                .ForMember(dest => dest.MainCategoryName, opt => opt.MapFrom(src => src.MainCategory.MainCategoryName))
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategory.SubCategoryName))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName))
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.ModelName));
        }
    }
}
