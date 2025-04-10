using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.Supplier
{
    public class GetSupplierDto : IMapFrom<Inv.Domain.Entities.Supplier>
    {
        [Required]
        public int SupplierSerialId { get; set; }
        [Required]
        public string? SupplierName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.Supplier, GetSupplierDto>();
        }
    }
}
