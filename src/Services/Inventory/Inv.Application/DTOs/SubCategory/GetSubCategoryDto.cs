using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.SubCategory
{
    public class GetSubCategoryDto : IMapFrom<Inv.Domain.Entities.SubCategory>
    {
        [Required]
        public int SubCategorySerialID { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? SubCategoryName { get; set; }
        public int MainCategorySerialID { get; set; }
        public string? MainCategoryName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.SubCategory, GetSubCategoryDto>()
                .ForMember(dest => dest.MainCategoryName, opt => opt.MapFrom(src => src.MainCategory.MainCategoryName)) 
                ;
        }
    }
}
