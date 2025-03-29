using AutoMapper;
using Inv.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;


namespace Inv.Application.DTOs.Store
{
    public class CreateStoreDto: IMapFrom<Inv.Domain.Entities.Store>
    {
        [Required]
        public int? ComSerialID { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        [StringLength(30, ErrorMessage = "The store name cannot exceed 30 characters. ")]
        public string? StoreName { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateStoreDto, Inv.Domain.Entities.Store>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
