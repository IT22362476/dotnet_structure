using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.UOMConversion
{
    public class GetUomConversionsWithPaginationDto : IMapFrom<Inv.Domain.Entities.UOMConversion>
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public int UOMConvSerialID { get; set; }
        [Required]
        public int UOMSerialID { get; set; }
        [Required]
        public int UOMToID { get; set; }
        [Required]
        public int ConversionRate { get; set; }
        [Required]
        public string? UOMName { get; set; }
        public string? UOMNameTo { get; set; }
        public string? UOMDescription { get; set; }
        public string? UOMDescriptionTo { get; set; }
        public string? ConversionDescription { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Modified { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUOMConversionDto, Inv.Domain.Entities.UOMConversion>();
        }
    }
}
