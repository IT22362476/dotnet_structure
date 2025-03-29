using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.GRN
{
    public class GetGRNDto : IMapFrom<Inv.Domain.Entities.GRN>
    {
        [Required]
        public int GRNSerialID { get; set; } // Primary Key

        [Required]
        public string GRNNumber { get; set; } = string.Empty; // Generated from LastNumber

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.GRN, GetGRNDto>();
        }
    }
}
