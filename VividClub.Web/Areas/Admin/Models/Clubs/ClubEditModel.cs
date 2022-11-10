using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using AutoMapper;
using VividClub.Common.Mapping;
using VividClub.Data.Entities;

namespace VividClub.Web.Areas.Admin.Models.Clubs
{
    public class ClubEditModel : IMapFrom<Club>, IHaveCustomMapping
    {
        [Required]
        [MinLength(3), MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        
        [Display(Name = "Upload a photo")]
        public IFormFile Photo { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MinLength(3), MaxLength(50)]
        public string Description { get; set; }


        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Club, ClubEditModel>()
                .ForMember(u => u.Name, cfg => cfg.MapFrom(u => u.Name))
                .ForMember(u => u.Photo, cfg => cfg.MapFrom(u => new FormFile(new MemoryStream(u.Photo), 0, u.Photo.Length, "name", "fileName")))
                .ForMember(u => u.Description, cfg => cfg.MapFrom(u => u.Description));
        }



    }
}