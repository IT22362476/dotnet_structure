using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.DTOs.UOM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.Warehouse
{
    public class UpdateWarehouseDto:IMapFrom<Domain.Entities.Warehouse>
    {
        [Required]
        public int WHSerialID { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "The name cannot exceed 60 characters. ")]
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
            profile.CreateMap<UpdateWarehouseDto, Inv.Domain.Entities.Warehouse>();

        }
    }
}
