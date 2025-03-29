using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inv.Application.Common.Mappings;
using AutoMapper;
using Inv.Application.DTOs.Model;

namespace Inv.Application.DTOs.SubCategory
{
    public class CreateSubCategoryDto : IMapFrom<Inv.Domain.Entities.SubCategory>
    {
      
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "max 25 chars")]
        public string? SubCategoryName { get; set; }
        public int MainCategorySerialID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSubCategoryDto, Inv.Domain.Entities.SubCategory>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
