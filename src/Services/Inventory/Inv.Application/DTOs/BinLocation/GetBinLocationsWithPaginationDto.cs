using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.DTOs.Rack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.BinLocation
{
    public class GetBinLocationsWithPaginationDto : IMapFrom<Domain.Entities.BinLocation>
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public int BinLctnSerialID { get; set; }
        [Required]
        public string? BinName { get; set; }
        [Required]
        public string? BinLctn { get; set; }
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
        public int Row { get; set; }
        [Required]
        public string? Column { get; set; }
        [Required]
        public int? Compartment { get; set; }
        [Required]
        public int? ComSerialID { get; set; }

        public string? CompanyName { get; set; }

        [Required]
        public int WHSerialID { get; set; }

        public string? WHName { get; set; }
        [Required]
        public int StoreSerialID { get; set; }

        public string? StoreName { get; set; }
        [Required]
        public int ZoneSerialID { get; set; }
        public string? ZoneName { get; set; }
        [Required]
        public int ItemSerialID { get; set; }
        [Required]
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemPartNo { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetBinLocationsWithPaginationDto, Inv.Domain.Entities.BinLocation>();
        } 
    }
}
