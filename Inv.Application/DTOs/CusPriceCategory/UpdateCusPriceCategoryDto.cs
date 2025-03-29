using AutoMapper;
using Inv.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Inv.Application.DTOs.CusPriceCategory
{
     public class UpdateCusPriceCategoryDto : IMapFrom<Inv.Domain.Entities.CusPriceCategory>
    {
        [Required]
        public int CusPriceCatSerialID { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "max 25 chars")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9]*(?: [A-Z0-9][A-Za-z0-9]*)*$", ErrorMessage = "start with a capital, letters,digits & spaces only.")]

        public string? CusPriceCatName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCusPriceCategoryDto, Inv.Domain.Entities.CusPriceCategory>();

        }
    }
}
