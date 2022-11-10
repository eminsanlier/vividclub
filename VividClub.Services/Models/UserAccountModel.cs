using AutoMapper;
using System.Collections.Generic;
using VividClub.Common.Mapping;
using VividClub.Data.Entities;
using VividClub.Services.Infrastructure.CustomDataStructures;

namespace VividClub.Services.Models
{
    public class UserAccountModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; }

        public PaginatedList<PostModel> Posts { get; set; } = null;

        public IEnumerable<ReceivedClubRequestModel> ClubRequestReceived { get; set; } = new List<ReceivedClubRequestModel>();

        public IEnumerable<SentClubRequestModel> ClubRequestSent { get; set; } = new List<SentClubRequestModel>();

        public IEnumerable<EventModel> Events { get; set; } = new List<EventModel>();


        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, UserAccountModel>()
                .ForMember(u => u.Posts, cfg => cfg.Ignore());

        }
    }
}