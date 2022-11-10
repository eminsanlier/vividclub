using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using VividClub.Common.Mapping;
using VividClub.Data.Entities;

namespace VividClub.Services.Models
{
    public class ClubModel : IMapFrom<Club>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AdminId { get; set; }

        public string Description { get; set; }

        public byte[] Photo { get; set; }

        //public ICollection<UserModel> Members { get; set; }

        public ICollection<Topic> Topics { get; set; }

        public ICollection<ClubModel> SubClubs { get; set; }

        public int MembersCount { get; set; }

        public List<string> MemberId { get; set; } = new List<string>();


        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Club, ClubModel>()
                //.ForMember(p => p.Posts, cfg => cfg.MapFrom(p => Mapper.Map<IEnumerable<PostModel>>(p.Posts)))
                .ForMember(e => e.MemberId, cfg => cfg.MapFrom(e => e.Members.Select(p => p.Id).ToList()))
                .ForMember(e => e.MembersCount, cfg => cfg.MapFrom(e => e.Members.Count));
            //.ForMember(p => p.SubClubs, cfg => cfg.MapFrom(p => Mapper.Map<IEnumerable<SubClubModel>>(p.SubClubs)))
            //.ForMember(p => p.Members, cfg => cfg.MapFrom(p => Mapper.Map<IEnumerable<UserModel>>(p.Members)));
        }
    }
}