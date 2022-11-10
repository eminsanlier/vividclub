using Microsoft.AspNetCore.Http;
using VividClub.Services.Infrastructure.CustomDataStructures;
using VividClub.Services.Models;

namespace VividClub.Services
{
    public interface IPostService : IService
    {
        void Create(string userId, string text, IFormFile photo);

        void Edit(int postId, string text, IFormFile photo);

        bool Exists(int id);

        bool UserIsAuthorizedToEdit(int postId, string userId);

        PaginatedList<PostModel> PostsByUserId(string userId, int pageIndex, int pageSize);

        PaginatedList<PostModel> ClubPostsByUserId(string userId, int pageIndex, int pageSize);

        PostModel PostById(int postId);

        void Delete(int postId);

        void Like(int postId);
    }
}