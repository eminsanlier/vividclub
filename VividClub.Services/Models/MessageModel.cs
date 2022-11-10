using AutoMapper;
using System;
using VividClub.Common.Mapping;
using VividClub.Data.Entities;

namespace VividClub.Services.Models
{
    public class MessageModel : IMapFrom<Message>, IHaveCustomMapping
    {
        public string SenderId { get; set; }

        public string MessageText { get; set; }

        public DateTime DateSent { get; set; }

        public bool IsSeen { get; set; }

        public string SenderFullName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Message, MessageModel>()
                 .ForMember(m => m.SenderFullName, cfg => cfg.MapFrom(m => m.Sender.FirstName + " " + m.Sender.LastName));
        }
    }
}