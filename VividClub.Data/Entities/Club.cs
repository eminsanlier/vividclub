using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VividClub.Data.Entities
{
    public class Club
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(DataConstants.MaxTitleLength)]
        public string Name { get; set; }
        
        public byte[] Photo { get; set; }
        
        [MaxLength(DataConstants.MaxDescriptionLength)]
        public string Description { get; set; }

        public User Admin { get; set; }
        
        public virtual ICollection<User> Members { get; set; } 

        public virtual ICollection<Topic> Topics { get; set; } 

        public virtual ICollection<Event> Events { get; set; } 

        public virtual ICollection<Comment> Comments { get; set; } 

        public virtual ICollection<Rating> Ratings { get; set; } 

        public bool IsDeleted { get; set; }
        
        public int ParentClubId { get; set; }
       
        
        
    }
}
