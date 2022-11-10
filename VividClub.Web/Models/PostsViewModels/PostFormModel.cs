using Microsoft.AspNetCore.Http;
using VividClub.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace VividClub.Web.Models.PostsViewModels
{
    public class PostFormModel
    {
        [Required]
        [Display(Name = "What do you think?")]
        public string Text { get; set; }


        [Display(Name = "Upload a photo")]
        public IFormFile Photo { get; set; }
    }
}