using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inv.Application.Common.Mappings;
using AutoMapper;
using Inv.Application.DTOs.ItemType;

namespace Inv.Application.DTOs.Model
{
    public class CreateModelDto : IMapFrom<Inv.Domain.Entities.Model>
    {
      
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? ModelName { get; set; }
        [Required]
        public int BrandSerialID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateModelDto, Inv.Domain.Entities.Model>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
