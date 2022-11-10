using System.Collections.Generic;
using VividClub.Services.Models;

namespace VividClub.Services
{
    public interface ICommentService : IService
    {
        void Create(string commentText, string userId, int postId);

        void DeleteCommentsByClubId(int clubId);

        IEnumerable<CommentModel> CommentsByClubId(int clubId);
    }
}