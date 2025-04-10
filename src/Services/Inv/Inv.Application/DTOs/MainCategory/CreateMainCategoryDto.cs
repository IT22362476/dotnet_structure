using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.MainCategory
{
    public class CreateMainCategoryDto : IMapFrom<Inv.Domain.Entities.MainCategory>
    {
       
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "max 25 chars")]
        public string? MainCategoryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMainCategoryDto, Inv.Domain.Entities.MainCategory>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
