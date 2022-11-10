﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using VividClub.Common.Mapping;
using VividClub.Data.Entities;

namespace VividClub.Services.Models
{
    public class EventModel : IMapFrom<Event>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public DateTime DateStarts { get; set; }

        public DateTime DateEnds { get; set; }

        public int ParticipantsCount { get; set; }

        public List<string> ParticipantId { get; set; } = new List<string>();

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Event, EventModel>()
                .ForMember(e => e.ParticipantId, cfg => cfg.MapFrom(e => e.Participants.Select(p => p.Id).ToList()))
                .ForMember(e => e.ParticipantsCount, cfg => cfg.MapFrom(e => e.Participants.Count));
        }
    }
}