using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.CompatibleItem
{
     public class CreateCompatibleItemDto : IMapFrom<Inv.Domain.Entities.CompatibleItem>
    {
        [Required]
        public int ItemSerialID { get; set; } // ID of the item (foreign key)

        [Required]
        public int ItemCompatibleSerialID { get; set; } // Another ID for compatibility (foreign key)
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCompatibleItemDto, Inv.Domain.Entities.CompatibleItem>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
