using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VividClub.Data;
using VividClub.Data.Entities;
using VividClub.Services.Infrastructure;
using VividClub.Services.Infrastructure.CustomDataStructures;
using VividClub.Services.Models;

namespace VividClub.Services
{
    public class ClubService : IClubService
    {
        private readonly VividClubDbContext db;
        private readonly IPhotoService photoService;
        private readonly IPostService postService;
        private readonly IEventService eventService;
        private readonly IUserService userService;

        public ClubService(VividClubDbContext db, IPhotoService photoService, IPostService postService, IEventService eventService, IUserService userService)
        {
            this.db = db;
            this.photoService = photoService;
            this.postService = postService;
            this.eventService = eventService;
            this.userService = userService;
        }

        public bool ClubExists(int clubId) => this.db.Clubs.Any(u => u.Id == clubId && u.IsDeleted == false);

        public Club GetById(int id)
        {
            if (this.ClubExists(id))
            {
                return Mapper.Map<Club>(this.db.Clubs.Find(id));
            }

            return null;
        }

        public void AddClubPicture(IFormFile photo, int clubId)
        {
            if (this.ClubExists(clubId))
            {
                var club = this.db.Clubs.Find(clubId);
                club.Photo = this.photoService.PhotoAsBytes(photo);
                this.db.SaveChanges();
            }
        }

        public PaginatedList<ClubModel> All(int pageIndex, int pageSize)
        {
            var clubs = this.db.Clubs
                 .Where(u => u.IsDeleted == false)
                 .ProjectTo<ClubModel>().OrderByDescending(p => p.Name);

            return clubs != null ? PaginatedList<ClubModel>.Create(clubs.AsNoTracking(), pageIndex, pageSize) : null;
        }

        public PaginatedList<ClubModel> ClubsBySearchTerm(string searchTerm, int pageIndex, int pageSize)
        {
            var clubs = this.db.Clubs
                .Where(u => (u.Name.ToLower().Contains(searchTerm.ToLower())
                || u.Description.ToLower().Contains(searchTerm.ToLower()))
                && u.IsDeleted == false).ProjectTo<ClubModel>();

            return clubs != null ? PaginatedList<ClubModel>.Create(clubs.AsNoTracking(), pageIndex, pageSize) : null;
        }

        public void CreateClub(string name, string description, IFormFile photo)
        {
            User admin = this.db.Users.Where(u => u.UserName.Equals(ServiceConstants.AdminUserName)).FirstOrDefault();
            var club = new Club
            {
                Name = name,
                Description = description,
                Admin = admin,
            };

            if (photo != null)
                club.Photo = this.photoService.PhotoAsBytes(photo);

            this.db.Add(club);
            this.db.SaveChanges();
        }

        public void EditClub(int id, string name, string description, IFormFile photo)
        {
            var club = this.db.Clubs.Find(id);
            club.Name = name;
            club.Description = description;
            if (photo != null)
                club.Photo = this.photoService.PhotoAsBytes(photo);
            this.db.SaveChanges();

        }

        public void DeleteClub(int id)
        {
            var club = this.db.Clubs.Find(id);

            club.IsDeleted = true;

            this.db.SaveChanges();
        }

        public PaginatedList<ClubModel> GetUserClubs(string userId, int pageIndex, int pageSize)
        {
            var user = this.db.Users.Include(u => u.Clubs).Where(u => u.Id.Equals(userId)).FirstOrDefault();
            List<ClubModel> clubList = new List<ClubModel>();
            foreach (var record in user.Clubs)
            {
                clubList.AddRange(this.db.Clubs.Where(c => c.Id.Equals(record.Id)).ProjectTo<ClubModel>());
            }

            return clubList != null ? PaginatedList<ClubModel>.Create((clubList as IQueryable<ClubModel>).AsNoTracking(), pageIndex, pageSize) : null;

        }

        public void AddUserToClub(string userId, int clubId)
        {
            if (this.ClubExists(clubId))
            {
                var ev = this.db.Clubs
                    .Include(e => e.Members)
                    .FirstOrDefault(e => e.Id == clubId);

                if (!ev.Members.Any(p => p.Id == userId))
                {
                    ev.Members.Add(userService.GetUserById(userId));
                }

                this.db.SaveChanges();
            }
        }

        public ClubModel Details(int id)
        {
            if (this.ClubExists(id))
            {
                var ev = this.db.Clubs
                    .Include(e => e.Members)
                    .FirstOrDefault(e => e.Id == id);

                return Mapper.Map<ClubModel>(ev);
            }

            return null;
        }
    }
}