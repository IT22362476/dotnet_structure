using AutoMapper;
using Inv.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Inv.Application.DTOs.Warehouse
{
    public class CreateWarehouseDto:IMapFrom<Domain.Entities.Warehouse>
    {
        [Required]
        [StringLength(40, ErrorMessage = "The name cannot exceed 60 characters. ")]
        public string? WHName { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address1 { get; set; }

        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address2 { get; set; }

        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address3 { get; set; }

        [Required]
        public int ComSerialID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateWarehouseDto, Inv.Domain.Entities.Warehouse>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
