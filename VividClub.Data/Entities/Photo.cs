using System.ComponentModel.DataAnnotations;

namespace VividClub.Data.Entities
{
    public class Photo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxPhotoLength)]
        public byte[] PhotoAsBytes { get; set; }

        public User User { get; set; }

        public Post Post { get; set; }

        public bool IsProfilePicture { get; set; }
    }
}