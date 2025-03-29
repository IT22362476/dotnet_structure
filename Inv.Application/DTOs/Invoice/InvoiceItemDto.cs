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
    public class InvoiceItemDto : IMapFrom<Inv.Domain.Entities.InvoiceItem>
    {
        [Required]
        public int ItemSerialID { get; set; }
        [Required]
        public decimal BilledQty { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal BilledAmount { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.InvoiceItem, InvoiceItemDto>();
        }
    }
}
