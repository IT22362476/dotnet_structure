using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inv.Application.Common.Mappings;
using AutoMapper;

namespace Inv.Application.DTOs.Model
{
    public class UpdateModelDto : IMapFrom<Inv.Domain.Entities.Model>
    {
        [Required]
        public int ModelSerialID { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 25 characters.")]
        public string? ModelName { get; set; }
        [Required]
        public int BrandSerialID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateModelDto, Inv.Domain.Entities.Model>();
        }


    }


   
}

