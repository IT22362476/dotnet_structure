using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.Invoice
{
    public class GetInvoiceByGRNDto : IMapFrom<Inv.Domain.Entities.Invoice>
    {
        [Required]
        public int InvoiceSerialID { get; set; }
        [Required]
        public string InvoiceNumber { get; set; } = string.Empty; // Generated from LastNumber

        [Required]
        public int SupplierSerialID { get; set; }

        [Required]
        public string? SupplierName { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        public List<InvoiceItemDto> InvoiceItems { get; set; } = new List<InvoiceItemDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.Invoice, GetInvoiceByGRNDto>()
                .ForMember(d => d.SupplierSerialID, opt => opt.MapFrom(s => s.SupplierSerialID))
                .ForMember(d => d.InvoiceItems, opt => opt.MapFrom(s => s.InvoiceItems))
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Supplier.SupplierName))
                .ForMember(d => d.InvoiceDate, opt => opt.MapFrom(s => s.InvoiceDate))
                ;

            profile.CreateMap<Inv.Domain.Entities.InvoiceItem, InvoiceItemDto>();
        }
    }
}
