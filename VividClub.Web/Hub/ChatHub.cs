using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace VividClub.Web.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendCoreAsync("Send", new string[] { message });
        }

        public void Join(string groupName)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}