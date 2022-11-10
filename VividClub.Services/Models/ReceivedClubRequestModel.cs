using AutoMapper;
using VividClub.Common.Mapping;
using VividClub.Data.Entities;
using VividClub.Data.Entities.Enums;

namespace VividClub.Services.Models
{
    public class ReceivedClubRequestModel : IMapFrom<ClubRequest>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string SenderId { get; set; }

        public string SenderFullName { get; set; }

        public ClubRequestStatus ClubRequestStatus { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<ClubRequest, ReceivedClubRequestModel>()
                .ForMember(f => f.SenderFullName, cfg => cfg.MapFrom(f => f.Sender.FirstName + " " + f.Sender.LastName));
        }
    }
}