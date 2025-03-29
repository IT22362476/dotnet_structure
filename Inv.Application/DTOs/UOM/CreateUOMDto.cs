using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.DTOs.SubCategory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.UOM
{
    public class CreateUOMDto : IMapFrom<Inv.Domain.Entities.UOM>
    {
    
        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "The field must be between 1 and 10 characters.")]
        public string? UOMName { get; set; }
        [Required]
        public string? UOMDescription { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUOMDto, Inv.Domain.Entities.UOM>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
