using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace screensharing_service.Hubs
{
    public class DomHub : Hub
    {
        
        public async Task joinSession(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            //
            await Clients.Group(sessionId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {sessionId}.");
        }
        
        public async Task leaveSession(string sessionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);

            await Clients.Group(sessionId).SendAsync("Send", $"{Context.ConnectionId} has left the group {sessionId}.");
        }

        public async Task sendDom(string user,string sessionId,string dom)
        {
            
            await Clients.Group(sessionId).SendAsync("sentDom",user, dom);
        }
        
        
        public async Task sendMousePosition(string user,string sessionId,int x,int y)
        {
            await Clients.Group(sessionId).SendAsync("sentMousePosition",user, x,y);
        }

        public async Task sendScrollDown(string user,string sessionId)
        {
            await Clients.Group(sessionId).SendAsync("sentScrollDown",user);
        }
        
        public async Task sendScrollUp(string user,string sessionId)
        {
            await Clients.Group(sessionId).SendAsync("sentScrollUp",user);
        }

        public async Task sendScroll(string user,string sessionId,int vertical)
        {
            await Clients.Group(sessionId).SendAsync("sentScroll",user,vertical);
        }
        
    }
}