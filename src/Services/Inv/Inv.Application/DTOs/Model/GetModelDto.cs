using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.Model
{
    public class GetModelDto : IMapFrom<Inv.Domain.Entities.Model>
    {
        [Required]
        public int ModelSerialID { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? ModelName { get; set; }
        [Required]
        public int BrandSerialID { get; set; }
        public string? BrandName { get; set; }   
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.Model, GetModelDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName));
        }
    }
}
