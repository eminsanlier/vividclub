using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VividClub.Data.Entities
{
    public class Topic
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(DataConstants.MaxMessageLength)]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public Club Club { get; set; }

        public ICollection<Post> Posts { get; set; } 
    }
}
