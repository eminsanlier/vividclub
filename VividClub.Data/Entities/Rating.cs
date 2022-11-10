using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VividClub.Data.Entities
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Rate { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public Club Club { get; set; }
    }
}
