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
    public  class UpdateItemTypeDto : IMapFrom<Inv.Domain.Entities.ItemType>
    {
        [Required]
        public int ItemTypeSerialID { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "3-25 chars")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9]*(?: [A-Z0-9][A-Za-z0-9]*)*$", ErrorMessage = "start with a capital, letters,digits & spaces only.")]

        public string? ItemTypeName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateItemTypeDto, Inv.Domain.Entities.ItemType>();
        }
    }
}
