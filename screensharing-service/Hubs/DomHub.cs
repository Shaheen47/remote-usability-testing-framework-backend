using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace screensharing_service.Hubs
{
    public class DomHub : Hub
    {
        
        public async Task addToSession(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            //
            await Clients.Group(sessionId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {sessionId}.");
        }
        
        public async Task removeFromSession(string sessionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);

            await Clients.Group(sessionId).SendAsync("Send", $"{Context.ConnectionId} has left the group {sessionId}.");
        }

        public async Task sendDom(string user,string sessionId,string dom)
        {
            var i = 11;
            //
            await Clients.All.SendAsync("sentDom",user, dom);
        }
        
        
        public async Task sendMousePosition(string user,string sessionId,int x,int y)
        {
            await Clients.All.SendAsync("sentMousePosition",user, x,y);
        }

        public async Task sendScrollDown(string user,string sessionId)
        {
            await Clients.All.SendAsync("sentScrollDown",user);
        }
        
        public async Task sendScrollUp(string user,string sessionId)
        {
            await Clients.All.SendAsync("sentScrollUp",user);
        }

        public async Task sendScroll(string user,string sessionId,int vertical)
        {
            await Clients.All.SendAsync("sentScroll",user,vertical);
        }
        
    }
}