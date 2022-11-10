using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace VividClub.Web.Areas.Admin.Models.Clubs
{
    public class ClubCreateModel
    {
        [Required]
        [MinLength(3), MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Upload a photo")]
        public IFormFile Photo { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MinLength(3), MaxLength(50)]
        public string Description { get; set; }



    }
}