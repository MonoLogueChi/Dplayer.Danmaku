using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Danmaku.Utils.LiveDanmaku
{
    public class LiveDanmaku : Hub
    {
        public async Task Connection(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessage(string group, string user, string message)
        {
            await Clients.OthersInGroup(group).SendAsync("ReceiveMessage", user, message);
        }
    }
}