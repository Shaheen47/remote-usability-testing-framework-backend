using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace screensharing_service.Hubs
{
    public class ScreenMirroringHub: Hub,IScreenMirroringHub
    {



        public async Task closeSession(string sessionId)
        {
            await Clients.Group(sessionId).SendAsync("leaveSession");
        }

        public async Task joinSessionAsSubscriber(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            await Clients.OthersInGroup(sessionId).SendAsync("Send", "Subscriber"+ $"{Context.ConnectionId} has joined the group {sessionId}.");
        }
        
        public async Task joinSessionAsPublisher(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            await Clients.OthersInGroup(sessionId).SendAsync("Send", "Publisher"+ $"{Context.ConnectionId} has joined the group {sessionId}.");
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
        

        public async Task mouseUp(string sessionId)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("mouseUp");
        }

        public async Task mouseDown(string sessionId)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("mouseDown");
        }

        public async Task mouseOver(string sessionId, string elementXpath)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("mouseOver",elementXpath);
        }

        public async Task mouseOut(string sessionId, string elementXpath)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("mouseOut",elementXpath);
        }
        

        public async Task inputChanged(string sessionId, string elementXpath, string inputContent)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("inputChanged",elementXpath,inputContent);
        }
    }
}