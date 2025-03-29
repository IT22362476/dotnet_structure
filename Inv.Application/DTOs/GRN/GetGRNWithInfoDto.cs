using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.GRN
{
    public class GetGRNWithInfoDto : IMapFrom<Inv.Domain.Entities.GRN>
    {
        [Required]
        public int GRNSerialID { get; set; } // Primary Key

        [Required]
        public string GRNNumber { get; set; } = string.Empty; // Generated from LastNumber

        /*        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public decimal PurchasePrice { get; set; }
        [Required]
        public int SupplierSerialId { get; set; }
        [Required]
        public string? SupplierName { get; set; }*/

        [Required]
        public int InvoiceSerialID { get; set; } // Primary Key
        [Required]
        public string? InvoiceNumber { get; set; }  // Generated from LastNumber
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.GRN, GetGRNWithInfoDto>()
                .ForMember(d => d.InvoiceSerialID, opt => opt.MapFrom(s => s.Invoice.FirstOrDefault().InvoiceSerialID))
                .ForMember(d => d.InvoiceNumber, opt => opt.MapFrom(s => s.Invoice.FirstOrDefault().InvoiceNumber));
        }
    }
}
