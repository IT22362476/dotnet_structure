using AutoMapper;
using Inv.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;


namespace Inv.Application.DTOs.BinLocation
{
    public class UpdateBinLocationDto:IMapFrom<Inv.Domain.Entities.BinLocation>
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
        public int? Compartment { get; set; }

        [Required]
        public int ComSerialID { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        [Required]
        public int StoreSerialID { get; set; }

        [Required]
        public int ZoneSerialID { get; set; }

        [Required]
        public int RackSerialID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBinLocationDto, Inv.Domain.Entities.BinLocation>();

        }
    }
}
