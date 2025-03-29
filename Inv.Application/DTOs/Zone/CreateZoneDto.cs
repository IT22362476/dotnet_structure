using AutoMapper;
using Inv.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inv.Application.DTOs.Zone
{
    public class CreateZoneDto : IMapFrom<Inv.Domain.Entities.Zone>
    {
         [Required]
        [Display(Name = "Zone Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "max 30 chars")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s]*$", ErrorMessage = "start with a capital, letters & spaces only.")]

        public string? ZoneName { get; set; }

        [Required]
        public int ComSerialID { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        [Required]
        public int StoreSerialID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateZoneDto, Inv.Domain.Entities.Zone>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
