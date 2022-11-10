using System;
using VividClub.Common.Mapping;
using VividClub.Data.Entities;
using VividClub.Data.Entities.Enums;

namespace VividClub.Services.Models
{
    public class UserModel : IMapFrom<User>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}