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
    public class UpdateUOMConversionDto : IMapFrom<Inv.Domain.Entities.UOMConversion>
    {
        [Required]
        public int UOMConvSerialID { get; set; }
        [Required]
        public int UOMSerialID { get; set; }
        [Required]
        public int UOMToID { get; set; }
        [Required]
        public decimal ConversionRate { get; set; }
        [Required]
        public string? ConversionDescription { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUOMConversionDto, Inv.Domain.Entities.UOMConversion>();
        }
    }
}
