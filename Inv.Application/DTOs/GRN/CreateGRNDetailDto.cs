using AutoMapper;
using Inv.Application.Common.Mappings;
using Inv.Domain.Entities;

namespace Inv.Application.DTOs.GRN
{
    public class CreateGRNDetailDto : IMapFrom<GRNDetail>
    {
        public int LineNumber { get; set; }

        public int ItemSerialID { get; set; }

        public int SystemPOSerialID { get; set; }

        public string? BatchNumber { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public decimal WarrentyPeriod { get; set; }

        public int? UOMSerialID { get; set; }

        public Condition Condition { get; set; }

        public decimal Qty { get; set; }

        public decimal FOCQty { get; set; }

        public decimal BatchBalQty { get; set; }

        public int? AssetCount { get; set; }

        public string Remarks { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateGRNDetailDto, GRNDetail>();
        }
    }
}
