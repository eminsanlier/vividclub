namespace VividClub.Services.Models
{
    //public class SubClubModel : IMapFrom<SubClub>, IHaveCustomMapping
    //{
    //    public int Id { get; set; }

    //    public string Name { get; set; }

    //    public string AdminId { get; set; }

    //    public string Description { get; set; }

    //    public byte[] Photo { get; set; }

    //    public ICollection<UserModel> Members { get; set; }

    //    public ICollection<PostModel> Posts { get; set; }

    //    public ICollection<EventModel> Events { get; set; }


    //    public void ConfigureMapping(Profile profile)
    //    {
    //        profile.CreateMap<SubClub, SubClubModel>()
    //            .ForMember(p => p.Posts, cfg => cfg.MapFrom(p => Mapper.Map<IEnumerable<PostModel>>(p.Posts)))
    //            .ForMember(p => p.Events, cfg => cfg.MapFrom(p => Mapper.Map<IEnumerable<EventModel>>(p.Events)))
    //            .ForMember(p => p.Members, cfg => cfg.MapFrom(p => Mapper.Map<IEnumerable<UserModel>>(p.Members)));
    //    }
    //}
}