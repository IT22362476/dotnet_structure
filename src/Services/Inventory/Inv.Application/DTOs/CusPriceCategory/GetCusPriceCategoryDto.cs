using AutoMapper;
using Inv.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Inv.Application.DTOs.CusPriceCategory
{
    public class GetCusPriceCategoryDto : IMapFrom<Inv.Domain.Entities.CusPriceCategory>
    {
        [Required]
        public int CusPriceCatSerialID { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? CusPriceCatName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inv.Domain.Entities.CusPriceCategory, GetCusPriceCategoryDto>();

        }
    }
}
