using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Web.Hubs
{
    public class GameHub : Hub
    {
        public string GetUserName()
        {
            return Context.User.Identity.Name ?? "Not registered user.";
        }
        public async Task SendLobbyMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveLobbyMessage", GetUserName(), message);
        }
    }
}
