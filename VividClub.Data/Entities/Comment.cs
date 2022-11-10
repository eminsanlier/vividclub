using System;
using System.ComponentModel.DataAnnotations;

namespace VividClub.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxMessageLength)]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public Club Club { get; set; }
    }
}