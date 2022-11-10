using AutoMapper;
using VividClub.Common.Mapping;
using VividClub.Data.Entities;
using VividClub.Data.Entities.Enums;

namespace VividClub.Services.Models
{
    public class SentClubRequestModel : IMapFrom<ClubRequest>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ClubRequestStatus ClubRequestStatus { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<ClubRequest, SentClubRequestModel>()
                .ForMember(f => f.Name, cfg => cfg.MapFrom(f => f.Sender.UserName + " " + f.Name));
        }
    }
}