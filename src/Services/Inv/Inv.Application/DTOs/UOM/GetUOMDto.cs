using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.UOM
{
    public class GetUOMDto : IMapFrom<Inv.Domain.Entities.UOM>
    {
        [Required]
        public int UOMSerialID { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "The field must be between 1 and 25 characters.")]
        public string? UOMName { get; set; }
        [Required]
        public string? UOMDescription { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.UOM, GetUOMDto>();
        }
    }
}
