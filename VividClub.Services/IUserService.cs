using Microsoft.AspNetCore.Http;
using System;
using VividClub.Data.Entities;
using VividClub.Data.Entities.Enums;
using VividClub.Services.Infrastructure.CustomDataStructures;
using VividClub.Services.Models;

namespace VividClub.Services
{
    public interface IUserService : IService
    {
        UserModel GetById(string id);

        User GetUserById(string id);

        void AddProfilePicture(IFormFile photo, string userId);

        bool UserExists(string userId);

        UserAccountModel UserDetails(string userId, int pageIndex, int pageSize);

        UserAccountModel UserDetailsAndPosts(string userId, int pageIndex, int pageSize);


        bool CheckIfDeleted(string userId);

        bool CheckIfDeletedByUserName(string username);


        PaginatedList<UserListModel> UsersBySearchTerm(string searchTerm, int pageIndex, int pageSize);

        PaginatedList<UserListModel> All(int pageIndex, int pageSize);

        object GetUserFullName(string id);

        void EditUser(string id, string firstName, string lastName, string email, string username, Gender modelGender, DateTime BirthDate);

        void DeleteUser(string id);

        bool UserExistsByEmail(string email);
    }
}