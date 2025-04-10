using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.DTOs.UOM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.Store
{
    public class UpdateStoreDto:IMapFrom<Inv.Domain.Entities.Store>
    {
        [Required]
        public int StoreSerialID { get; set; }

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
            profile.CreateMap<UpdateStoreDto, Inv.Domain.Entities.Store>();

        }
    }
}
