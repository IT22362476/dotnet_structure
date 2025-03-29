using AutoMapper;
using Inv.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Inv.Application.DTOs.CompatibleItem
{
    public class UpdateCompatibleItemDto : IMapFrom<Inv.Domain.Entities.CompatibleItem>
    {
        [Required]
        public int CompatibleItemSerialID { get; set; } // Primary key for the CompatibleItem

        [Required]
        public int ItemSerialID { get; set; } // ID of the item (foreign key)

        [Required]
        public int ItemCompatibleSerialID { get; set; } // Another ID for compatibility (foreign key)

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCompatibleItemDto, Inv.Domain.Entities.CompatibleItem>();

        }
    }
}
