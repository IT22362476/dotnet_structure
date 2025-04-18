using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Domain.Entities;

namespace Inv.Application.DTOs.GRN
{
    public class GetGRNHeaderByIdDto : IMapFrom<GRNHeader>
    {
        [Required]
        public int GRNHeaderSerialID { get; set; }

        [Required]
        public int GRNID { get; set; }

        [Required]
        public int CompSerialID { get; set; }

        [Required]
        public int SupplierSerialID { get; set; }

        [Required]
        public string? SupplierInvoiceNumber { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        [Required]
        public int StoreSerialID { get; set; }

        [Required]
        public bool Printed { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public List<GetGRNDetailDto> GRNDetails { get; set; } = new List<GetGRNDetailDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GRNHeader, GetGRNHeaderByIdDto>()
                .ForMember(dest => dest.GRNDetails, opt => opt.MapFrom(src => src.GRNDetails));
        }

    }
}
