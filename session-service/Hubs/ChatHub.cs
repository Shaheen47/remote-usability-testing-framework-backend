using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace session_service.Hubs
{
    public class ChatHub: Hub,IChatHub
    {
        public async Task joinSession(string chatSessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatSessionId);
            await Clients.Group(chatSessionId).SendAsync("userJoined", $"{Context.ConnectionId} has joined the group {chatSessionId}.");
        }

        public async Task leaveSession(string chatSessionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatSessionId);
            await Clients.Group(chatSessionId).SendAsync("userLeft", $"{Context.ConnectionId} has left the group {chatSessionId}.");
        }

        public async Task sendMessage(string chatSessionId, string senderName, string message)
        {
            await Clients.Group(chatSessionId).SendAsync("messageSent", senderName, message);
        }
    }
}