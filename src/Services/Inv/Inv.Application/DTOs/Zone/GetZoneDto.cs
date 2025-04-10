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

namespace Inv.Application.DTOs.Zone
{
    public class GetZoneDto : IMapFrom<Inv.Domain.Entities.Zone>
    {
        [Key]
        public int ZoneSerialID { get; set; }

        [Required]
        [Display(Name = "Zone Name")]
        [StringLength(30, ErrorMessage = "The zone name cannot exceed 30 characters. ")]
        public string? ZoneName { get; set; }

        [Required]
        public int ComSerialID { get; set; }
   
        [Required]
        public int WHSerialID { get; set; }

        [Required]
        public int StoreSerialID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.Zone, GetZoneDto>();

        }
    }
}
