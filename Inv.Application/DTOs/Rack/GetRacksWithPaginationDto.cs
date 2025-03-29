using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.DTOs.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.Rack
{
    public class GetRacksWithPaginationDto:IMapFrom<Domain.Entities.Rack>
    {
        [Required]
        public long Id { get; set; }
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

        [Required]
        public int Rows { get; set; }

        [Required]
        public int Columns { get; set; }

        public int? Compartments { get; set; }

        [Required]
        public int? ComSerialID { get; set; }

        public string? CompanyName { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        public string? WHName { get; set; }
        [Required]
        public int StoreSerialID { get; set; }

        public string? Store { get; set; }
        [Required]
        public int ZoneSerialID { get; set; }
        public string? ZoneName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Modified { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetRacksWithPaginationDto, Inv.Domain.Entities.Rack>();
        }
    }
}
