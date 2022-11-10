﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VividClub.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxDescriptionLength)]
        public string Location { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public DateTime DateStarts { get; set; }

        [Required]
        public DateTime DateEnds { get; set; }

        public virtual ICollection<User> Participants { get; set; }
    }
}