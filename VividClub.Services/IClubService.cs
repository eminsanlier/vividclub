using Microsoft.AspNetCore.Http;
using VividClub.Data.Entities;
using VividClub.Services.Infrastructure.CustomDataStructures;
using VividClub.Services.Models;

namespace VividClub.Services
{
    public interface IClubService : IService
    {
        Club GetById(int id);

        void AddClubPicture(IFormFile photo, int clubId);

        PaginatedList<ClubModel> ClubsBySearchTerm(string searchTerm, int pageIndex, int pageSize);

        PaginatedList<ClubModel> All(int pageIndex, int pageSize);

        PaginatedList<ClubModel> GetUserClubs(string userId, int pageIndex, int pageSize);

        void CreateClub(string name, string description, IFormFile photo);

        void EditClub(int id, string name, string description, IFormFile photo);

        void DeleteClub(int id);

        bool ClubExists(int userId);

        void AddUserToClub(string userId, int ClubId);

        ClubModel Details(int id);
    }
}