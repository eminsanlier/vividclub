using VividClub.Common.Mapping;
using VividClub.Data.Entities;

namespace VividClub.Services.Models
{
    public class ClubListModel : IMapFrom<Club>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Photo { get; set; }
    }
}
