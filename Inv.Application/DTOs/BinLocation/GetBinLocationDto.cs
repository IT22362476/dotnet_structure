using AutoMapper;
using Inv.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;


namespace Inv.Application.DTOs.BinLocation
{
    public class GetBinLocationDto: IMapFrom<Inv.Domain.Entities.BinLocation>
    {
        [Required]
        public int BinLctnSerialID { get; set; }
        [Required]
        public string? BinName { get; set; }
        [Required]
        public string? BinLctn { get; set; }
        [Required]
        public int ItemSerialID { get; set; }
        [Required]
        public int Row { get; set; }

        [Required]
        public string? Column { get; set; }
        [Required]
        public int Columns { get; set; }
        [Required]
        public int? Compartment { get; set; }

        [Required]
        public int ComSerialID { get; set; }

        [Required]
        public int WHSerialID { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The name cannot exceed 60 characters. ")]
        public string? WHName { get; set; }
        [Required]
        public int StoreSerialID { get; set; }
        [Required]
        [Display(Name = "Store Name")]
        [StringLength(30, ErrorMessage = "The store name cannot exceed 30 characters. ")]
        public string? StoreName { get; set; }
        [Required]
        public int ZoneSerialID { get; set; }
        [Required]
        [Display(Name = "Zone Name")]
        [StringLength(30, ErrorMessage = "The zone name cannot exceed 30 characters. ")]
        public string? ZoneName { get; set; }
        [Required]
        public int RackSerialID { get; set; }
        [Required]
        [Display(Name = "Rack Name")]
        [StringLength(30, ErrorMessage = "The rack name cannot exceed 30 characters. ")]
        public string? RackName { get; set; }
        [Required]
        [Display(Name = "Rack Code")]
        [StringLength(10, ErrorMessage = "The rack code cannot exceed 10 characters. ")]
        public string? RackCode { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.BinLocation,GetBinLocationDto>()
                .ForMember(dest => dest.Columns, opt => opt.MapFrom(src => src.Rack.Columns))
                .ForMember(dest => dest.WHName, opt => opt.MapFrom(src => src.Warehouse.WHName))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.StoreName))
                .ForMember(dest => dest.ZoneName, opt => opt.MapFrom(src => src.Zone.ZoneName))
                .ForMember(dest => dest.RackName, opt => opt.MapFrom(src => src.Rack.RackName))
                .ForMember(dest => dest.RackCode, opt => opt.MapFrom(src => src.Rack.RackCode))

                ;

        }
    }
}
