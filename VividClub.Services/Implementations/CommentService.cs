using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using VividClub.Data;
using VividClub.Data.Entities;
using VividClub.Services.Models;

namespace VividClub.Services
{
    public class CommentService : ICommentService
    {
        private readonly VividClubDbContext db;
        private readonly IClubService clubService;

        public CommentService(VividClubDbContext db, IClubService clubService)
        {
            this.db = db;
            this.clubService = clubService;
        }

        public IEnumerable<CommentModel> CommentsByClubId(int clubId)
        {
            //TO DO - implement some paging in case there are many comments
            return this.db.Comments.Where(c => c.Club.Id == clubId).OrderByDescending(c => c.Date).ProjectTo<CommentModel>().ToList();
        }

        public void Create(string commentText, string userId, int clubId)
        {
            var comment = new Comment
            {
                Date = DateTime.UtcNow,
                Text = commentText,
                User = this.db.Users.Find(userId),
                Club = clubService.GetById(clubId)
            };

            this.db.Comments.Add(comment);
            this.db.SaveChanges();
        }

        public void DeleteCommentsByClubId(int clubId)
        {
            var comments = this.db.Comments.Where(c => c.Club.Id == clubId);

            foreach (var comment in comments)
            {
                this.db.Remove(comment);
            }

            this.db.SaveChanges();
        }
    }
}