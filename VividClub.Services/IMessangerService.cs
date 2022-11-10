using VividClub.Services.Infrastructure.CustomDataStructures;
using VividClub.Services.Models;

namespace VividClub.Services
{
    public interface IMessangerService : IService
    {
        void Create(string senderId, string receiverId, string text);

        PaginatedList<MessageModel> AllByUserIds(string userId, string otherUserId, int pageIndex, int pageSize);
    }
}