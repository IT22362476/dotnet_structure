using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Application.DTOs.UOMConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.DTOs.Item
{
    public class GetItemsWithPaginationDto: IMapFrom<Inv.Domain.Entities.Item>
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public int ItemSerialID { get; set; }
        [Required]
        public int ItemID { get; set; }
        [Required]
        public string? ItemCode { get; set; }
        [Required]
        public int? ItemTypeSerialID { get; set; }
        [Required]
        public string? ItemDes { get; set; }
        [Required]
        public int? MainCategorySerialID { get; set; }
        [Required]
        public int? SubCategorySerialID { get; set; }
        [Required]
        public int? ModelSerialID { get; set; }
        [Required]
        public int? BrandSerialID { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? ItemPartNo { get; set; }
        public string? Article { get; set; }
        public string? Remarks { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string? Guage { get; set; }
        public string? Construction { get; set; }
        public string? SpecialFeatures { get; set; }
        [Required]
        public int? UOMSerialID { get; set; }
        public string? MainCategoryName { get; set; }
        public string? ItemTypeName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? ModelName { get; set; }
        public string? BrandName { get; set; }
        public string? UOMName { get; set; }
        public int? ApprovedBy { get; set; }
        public string? Approved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Modified { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetItemsWithPaginationDto, Inv.Domain.Entities.Item>();
        }
    }
}
