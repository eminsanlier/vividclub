using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VividClub.Data;
using VividClub.Data.Entities;
using VividClub.Data.Entities.Enums;
using VividClub.Services.Infrastructure;
using VividClub.Services.Infrastructure.CustomDataStructures;
using VividClub.Services.Models;

namespace VividClub.Services
{
    public class UserService : IUserService
    {
        private readonly VividClubDbContext db;
        private readonly IPhotoService photoService;
        private readonly IPostService postService;
        private readonly IEventService eventService;

        public UserService(VividClubDbContext db, IPhotoService photoService, IPostService postService, IEventService eventService)
        {
            this.db = db;
            this.photoService = photoService;
            this.postService = postService;
            this.eventService = eventService;
        }

        public void AddProfilePicture(IFormFile photo, string userId)
        {
            if (this.UserExists(userId))
            {
                var user = this.db.Users.Find(userId);
                user.ProfilePicture = this.photoService.PhotoAsBytes(photo);
                this.db.SaveChanges();
            }
        }

        public UserAccountModel UserDetails(string userId, int pageIndex, int pageSize)
        {
            if (this.UserExists(userId))
            {
                var userPosts = this.postService.PostsByUserId(userId, pageIndex, pageSize);
                var userAccountModel = db
                    .Users
                    .Where(u => u.Id == userId)
                    .ProjectTo<UserAccountModel>()
                    .FirstOrDefault();



                userAccountModel.Posts = userPosts;
                userAccountModel.Events = this.eventService.UpcomingThreeEvents();

                return userAccountModel;
            }
            else
            {
                return null;
            }
        }

        public virtual UserAccountModel UserDetailsAndPosts(string userId, int pageIndex, int pageSize)
        {
            if (this.UserExists(userId))
            {
                var userAccountModel = db
                    .Users
                    .Where(u => u.Id == userId)
                    .ProjectTo<UserAccountModel>()
                    .FirstOrDefault();

                userAccountModel.Posts = this.postService.ClubPostsByUserId(userId, pageIndex, pageSize);
                userAccountModel.Events = this.eventService.UpcomingThreeEvents();

                return userAccountModel;
            }
            else
            {
                return null;
            }
        }

        public bool UserExists(string userId) => this.db.Users.Any(u => u.Id == userId && u.IsDeleted == false);

        public PaginatedList<UserListModel> UsersBySearchTerm(string searchTerm, int pageIndex, int pageSize)
        {
            var users = this.db.Users
                .Where(u => (u.FirstName.ToLower().Contains(searchTerm.ToLower())
                || u.LastName.ToLower().Contains(searchTerm.ToLower())
                || u.UserName.ToLower().Contains(searchTerm.ToLower()))
                && u.UserName != ServiceConstants.AdminUserName
                && u.IsDeleted == false)
                .ProjectTo<UserListModel>();

            return users != null ? PaginatedList<UserListModel>.Create(users.AsNoTracking(), pageIndex, pageSize) : null;
        }

        public PaginatedList<UserListModel> All(int pageIndex, int pageSize)
        {
            var users = this.db.Users
                .Where(u => u.UserName != ServiceConstants.AdminUserName && u.IsDeleted == false)
                .ProjectTo<UserListModel>();

            return users != null ? PaginatedList<UserListModel>.Create(users.AsNoTracking(), pageIndex, pageSize) : null;
        }

        public object GetUserFullName(string id)
        {
            if (this.UserExists(id))
            {
                var user = this.db.Users.Find(id);
                return user.FirstName + " " + user.LastName;
            }
            return null;
        }

        public bool CheckIfDeleted(string userId)
        {
            throw new System.NotImplementedException();
        }

        public UserModel GetById(string id)
        {
            if (this.UserExists(id))
            {
                return Mapper.Map<UserModel>(this.db.Users.Find(id));
            }

            return null;
        }

        public User GetUserById(string id)
        {
            if (this.UserExists(id))
            {
                return this.db.Users.Find(id);
            }

            return null;
        }

        public void EditUser(string id, string firstName, string lastName, string email, string username,
            Gender modelGender, DateTime BirthDate)
        {
            var user = this.db.Users.Find(id);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserName = username;
            user.Email = email;
            user.gender = modelGender;
            user.BirthDate = BirthDate;
            this.db.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            var user = this.db.Users.Find(id);

            user.IsDeleted = true;

            this.db.SaveChanges();
        }

        public bool CheckIfDeletedByUserName(string username)
        {
            if (this.db.Users.Any(u => u.UserName == username))
            {
                return this.db.Users.FirstOrDefault(u => u.UserName == username).IsDeleted;
            }

            return true;
        }

        public bool UserExistsByEmail(string email)
        {
            return this.db.Users.Any(u => u.Email.Equals(email) && u.IsDeleted == false);
        }
    }
}