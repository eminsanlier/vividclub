using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using VividClub.Data;
using VividClub.Data.Entities;

namespace VividClub.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly VividClubDbContext db;


        public PhotoService(VividClubDbContext db)
        {
            this.db = db;
        }

        public int Create(IFormFile photo, string userId)
        {
            using (var memoryStream = new MemoryStream())
            {
                photo.CopyTo(memoryStream);

                var instanceOfPhoto = new Photo
                {
                    IsProfilePicture = false,
                    PhotoAsBytes = memoryStream.ToArray(),
                    User = this.db.Users.Find(userId)
                };

                db.Photos.Add(instanceOfPhoto);
                db.SaveChanges();

                return instanceOfPhoto.Id;
            }
        }

        public byte[] PhotoAsBytes(IFormFile photo)
        {
            byte[] photoAsBytes;

            using (var memoryStream = new MemoryStream())
            {
                photo.CopyTo(memoryStream);
                photoAsBytes = memoryStream.ToArray();
            };

            return photoAsBytes;
        }

        public bool PhotoExists(int photoId) => this.db.Photos.Any(p => p.Id == photoId);
    }
}