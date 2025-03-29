using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.Invoice
{

    public class GetInvoiceDto : IMapFrom<Inv.Domain.Entities.Invoice>
    {
        [Required]
        public int InvoiceSerialId { get; set; }
        [Required]
        public string InvoiceNumber { get; set; } = string.Empty; // Generated from LastNumber

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.Invoice, GetInvoiceDto>();
        }
    }
}
