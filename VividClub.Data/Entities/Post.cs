using VividClub.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VividClub.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxMessageLength)]
        public string Text { get; set; }

        public DateTime Date { get; set; }
        
        public int Likes { get; set; }

        public byte[] Photo { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public Topic Topic { get; set; }
    }
}