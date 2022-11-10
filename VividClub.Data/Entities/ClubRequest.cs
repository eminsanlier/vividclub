using VividClub.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System;

namespace VividClub.Data.Entities
{
    public class ClubRequest
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxTitleLength)]
        public string Name { get; set; }

        [MaxLength(DataConstants.MaxDescriptionLength)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public User Sender { get; set; }

        [Required]
        public ClubRequestStatus ClubRequestStatus { get; set; }
    }
}