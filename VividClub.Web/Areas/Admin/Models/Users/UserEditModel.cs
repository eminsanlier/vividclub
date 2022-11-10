﻿using System;
using System.ComponentModel.DataAnnotations;
using VividClub.Data.Entities.Enums;

namespace VividClub.Web.Areas.Admin.Models.Users
{
    public class UserEditModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MinLength(2), MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Username")]
        [MinLength(2), MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }


        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

    }
}