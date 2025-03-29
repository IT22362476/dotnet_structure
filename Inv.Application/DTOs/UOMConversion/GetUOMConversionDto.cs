using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inv.Application.Common.Mappings;
using AutoMapper;

namespace Inv.Application.DTOs.UOMConversion
{
    public class GetUOMConversionDto : IMapFrom<Inv.Domain.Entities.UOMConversion>
    {
        [Required]
        public int UOMConvSerialID { get; set; }
        [Required]
        public int UOMSerialID { get; set; }
        [Required]
        public int UOMToID { get; set; }
        [Required]
        public int ConversionRate { get; set; }
        [Required]
        public string? ConversionDescription { get; set; }
        [Required]
        public string? UOMDescription { get; set; }
        [Required]
        public string? UOMDescriptionTo { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.UOMConversion, GetUOMConversionDto>().
                ForMember(dest => dest.UOMDescriptionTo, opt => opt.MapFrom(src => src.UOMTo.UOMDescription))
                .ForMember(dest => dest.UOMDescription, opt => opt.MapFrom(src => src.UOM.UOMDescription))
                ;
        }
    }
}
