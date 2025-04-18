using AutoMapper;
using Inv.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;


namespace Inv.Application.DTOs.DelRecord
{

    public class CreateDelRecordDto : IMapFrom<Inv.Domain.Entities.DelRecord>
    {
       [Required]
        public string? DocTable { get; set; }
        [Required]
        public int? DocSerialID { get; set; }
        [Required]
        public string? Remarks { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateDelRecordDto, Inv.Domain.Entities.DelRecord>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                           .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                            ;

        }
    }
}
