using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Domain.Entities;

namespace Inv.Application.DTOs.GRN
{
    public class GetGRNDetailDto : IMapFrom<GRNDetail>
    {
        [Required]
        public int GRNDetailSerialID { get; set; }

        [Required]
        public int GRNHeaderSerialID { get; set; }

        [Required]
        public int LineNumber { get; set; }

        [Required]
        public string ItemCode { get; set; } = string.Empty;

        [Required]
        public string? BatchNumber { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Required]
        public decimal WarrentyPeriod { get; set; }

        [Required]
        public string? UOMName {  get; set; }

        [Required]
        public decimal Qty { get; set; }

        public decimal FOCQty { get; set; }

        public string Remarks { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GRNDetail, GetGRNDetailDto>()
                .ForMember(dest => dest.ItemCode, opt => opt.MapFrom(src => src.Item.ItemCode))
                .ForMember(dest => dest.UOMName, opt => opt.MapFrom(src => src.UOM.UOMName));
        }

    }
}
