using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using VividClub.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace VividClub.Data.Entities
{
    public class User : IdentityUser
    {
        [MinLength(DataConstants.NameMinLength), MaxLength(DataConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [MinLength(DataConstants.NameMinLength), MaxLength(DataConstants.NameMaxLength)]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [NotMapped]
        public int Age { get { return (int)((DateTime.Now - BirthDate).TotalDays / 365); } }

        public Gender gender { get; set; }

        public bool IsDeleted { get; set; } = false;

        //public IEnumerable<Photo> Photos { get; set; } = new List<Photo>();

        [Required]
        [MaxLength(DataConstants.MaxPhotoLength)]
        public byte[] ProfilePicture { get; set; }

        public virtual ICollection<Club> Clubs { get; set; } 

        public virtual ICollection<Topic> Topics { get; set; } 

        public virtual ICollection<Post> Posts { get; set; } 

        public virtual ICollection<Comment> Comments { get; set; } 

        public virtual ICollection<ClubRequest> ClubRequests { get; set; } 

        public virtual ICollection<Rating> Ratings { get; set; } 

        public virtual ICollection<Message> MessagesSent { get; set; } 

        public virtual ICollection<Message> MessagesReceived { get; set; } 


    }
}