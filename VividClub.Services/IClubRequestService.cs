namespace VividClub.Services
{
    public interface IClubRequestService : IService
    {
        void Create(string senderId, string name);

        void Accept(string senderId, string name);

        void Delete(string senderId, string name);

        void Decline(string senderId, string name);

        bool Exists(string senderId, string receiverId);
    }
}