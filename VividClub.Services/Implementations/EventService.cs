using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VividClub.Data;
using VividClub.Data.Entities;
using VividClub.Services.Models;

namespace VividClub.Services
{
    public class EventService : IEventService
    {
        private readonly VividClubDbContext db;

        public EventService(VividClubDbContext db)
        {
            this.db = db;
        }

        public void AddUserToEvent(string userId, int eventId)
        {
            if (this.Exists(eventId))
            {
                var ev = this.db.Events
                    .Include(e => e.Participants)
                    .FirstOrDefault(e => e.Id == eventId);

                if (!ev.Participants.Any(p => p.Id == userId))
                {
                    ev.Participants.Add(this.db.Users.Find(userId));
                }

                this.db.SaveChanges();
            }
        }

        public void Create(string imageUrl, string title, string location, string description, DateTime dateStarts, DateTime dateEnds, string creatorId)
        {
            var ev = new Event
            {
                ImageUrl = imageUrl,
                Title = title,
                Location = location,
                Description = description,
                DateEnds = dateEnds,
                DateStarts = dateStarts
            };
            ev.Participants.Add(this.db.Users.Find(creatorId));

            this.db.Events.Add(ev);
            this.db.SaveChanges();
        }

        public EventModel Details(int id)
        {
            if (this.Exists(id))
            {
                var ev = this.db.Events
                    .Include(e => e.Participants)
                    .FirstOrDefault(e => e.Id == id);

                return Mapper.Map<EventModel>(ev);
            }

            return null;
        }

        public bool Exists(int id) => this.db.Events.Any(e => e.Id == id);

        public IEnumerable<EventModel> UpcomingThreeEvents()
        {
            return this.db.Events.Where(e => e.DateEnds < DateTime.UtcNow).OrderBy(e => e.DateStarts).Take(3).ProjectTo<EventModel>().ToList();
        }
    }
}