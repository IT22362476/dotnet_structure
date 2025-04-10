using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.DTOs.UOM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.BinLocation
{
    public class CreateBinLocationDto:IMapFrom<Inv.Domain.Entities.BinLocation>
    {
        [Required]
        public string? BinName { get; set; }
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
        public string? BinLctn { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBinLocationDto, Inv.Domain.Entities.BinLocation>()
                            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
