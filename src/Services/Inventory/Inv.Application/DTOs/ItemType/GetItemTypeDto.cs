using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.ItemType
{
    public class GetItemTypeDto : IMapFrom<Inv.Domain.Entities.ItemType>
    {
        [Required]
        public int ItemTypeSerialID { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? ItemTypeName { get; set; }
 
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.ItemType, GetItemTypeDto>();
        }
    }
}
