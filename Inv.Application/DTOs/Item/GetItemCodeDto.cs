using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.Item
{
    public class GetItemCodeDto : IMapFrom<Inv.Domain.Entities.Item>
    {
        [Required]
        public int ItemSerialID { get; set; }
        [Required]
        public string? ItemCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.Item, GetItemCodeDto>();
        }
    }

}
