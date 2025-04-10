using Inv.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inv.Application.Common.Mappings;
using AutoMapper;
using Inv.Application.Features.Item.Command;

namespace Inv.Application.DTOs.Item
{
    public class UpdateItemDto : IMapFrom<Inv.Domain.Entities.Item>
    {
        [Required]
        public int ItemSerialID { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? ItemPartNo { get; set; }
        public string? Article { get; set; }
        public string? Remarks { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public string? Guage { get; set; }
        public string? Construction { get; set; }
        public string? SpecialFeatures { get; set; }
        [Required]
        public int? UOMSerialID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateItemCommand, Inv.Domain.Entities.Item>();
        }
    }
}
