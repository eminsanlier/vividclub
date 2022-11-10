using System.Linq;
using VividClub.Data;
using VividClub.Data.Entities;
using VividClub.Data.Entities.Enums;

namespace VividClub.Services
{
    public class ClubRequestService : IClubRequestService
    {
        private readonly VividClubDbContext db;
        private readonly IUserService userService;

        public ClubRequestService(VividClubDbContext db, IUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }

        public void Accept(string senderId, string receiverId)
        {
            if (this.Exists(senderId, receiverId) && this.userService.UserExists(senderId) && this.userService.UserExists(receiverId))
            {
                var clubRequest = db.ClubRequests.FirstOrDefault(fr => fr.Sender.Id == senderId);
                clubRequest.ClubRequestStatus = ClubRequestStatus.Accepted;
                this.db.SaveChanges();
            }
        }

        public void Create(string senderId, string name)
        {
            if (!this.Exists(senderId, name) && !string.IsNullOrEmpty(name))
            {
                var clubRequest = new ClubRequest
                {
                    Sender = userService.GetUserById(senderId),
                    Name = name,
                    ClubRequestStatus = ClubRequestStatus.Pending
                };

                this.db.ClubRequests.Add(clubRequest);
                this.db.SaveChanges();
            }
        }

        public void Decline(string senderId, string name)
        {
            if (this.Exists(senderId, name) && this.userService.UserExists(senderId))
            {
                var clubRequest = db.ClubRequests.FirstOrDefault(fr => fr.Sender.Id == senderId);
                this.db.Remove(clubRequest);
                this.db.SaveChanges();
            }
        }

        public void Delete(string senderId, string receiverId)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(string senderId, string receiverId) =>
             this.db.ClubRequests.Any(fr => fr.Sender.Id == senderId);
    }
}