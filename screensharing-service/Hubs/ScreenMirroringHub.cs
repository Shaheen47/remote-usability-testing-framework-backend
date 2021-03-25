using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace screensharing_service.Hubs
{
    public class ScreenMirroringHub: Hub,IScreenMirroringHub
    {
        public async Task joinSessionAsSubscriber(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            await Clients.OthersInGroup(sessionId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {sessionId}.");
        }
        
        public async Task joinSessionAsPublisher(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            await Clients.OthersInGroup(sessionId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {sessionId}.");
        }
        
        public async Task leaveSession(string sessionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);
            await Clients.OthersInGroup(sessionId).SendAsync("Send", $"{Context.ConnectionId} has left the group {sessionId}.");
        }

        public async Task sendDom(string sessionId,string dom)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("sentDom", dom);
        }
        
        
        public async Task sendMousePosition(string sessionId,int x,int y)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("sentMousePosition", x,y);
        }
        
        public async Task sendScroll(string sessionId,int vertical)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("sentScroll",vertical);
        }
    }
}