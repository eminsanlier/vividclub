using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VividClub.Data;
using VividClub.Data.Entities;
using VividClub.Services.Infrastructure.CustomDataStructures;
using VividClub.Services.Models;

namespace VividClub.Services
{
    public class MessangerService : IMessangerService
    {
        private readonly VividClubDbContext db;
        private readonly IUserService userService;

        public MessangerService(VividClubDbContext db, IUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }

        public List<MessageModel> All()
        {
            return this.db
                .Messages
                .ProjectTo<MessageModel>()
                .ToList();
        }

        public PaginatedList<MessageModel> AllByUserIds(string userId, string otherUserId, int pageIndex, int pageSize)
        {
            var messages = this.db
                .Messages
                .Where(m => (m.Sender.Id == userId && m.Receiver.Id == otherUserId) || (m.Sender.Id == otherUserId && m.Receiver.Id == userId))
                .OrderBy(m => m.DateSent)
                .ProjectTo<MessageModel>();

            return messages != null ? PaginatedList<MessageModel>.Create(messages.AsNoTracking(), pageIndex, pageSize) : null;
        }

        public void Create(string senderId, string receiverId, string text)
        {
            var message = new Message
            {
                Sender = userService.GetUserById(senderId),
                Receiver = userService.GetUserById(receiverId),
                DateSent = DateTime.UtcNow,
                IsSeen = false,
                MessageText = text
            };

            this.db.Add(message);
            this.db.SaveChanges();
        }
    }
}