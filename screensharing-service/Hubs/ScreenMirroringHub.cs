using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace screensharing_service.Hubs
{
    public class ScreenMirroringHub: Hub,IScreenMirroringHub
    {
        private ConcurrentDictionary<string, IList<string>> activeSessions;

        public ScreenMirroringHub()
        {
            activeSessions = new ConcurrentDictionary<string, IList<string>>();
        }
        public bool createSession(string sessionId)
        {
            if (activeSessions.ContainsKey(sessionId))
                return false;
            else
            {
                activeSessions.TryAdd(sessionId, new List<string>());
                return true;
            }
        }

        public bool closeSession(string sessionId)
        {
            if (activeSessions.ContainsKey(sessionId))
                return false;
            else
            {
                activeSessions.TryRemove(sessionId, out var sessionConnections);
                foreach (string connection in sessionConnections)
                {
                    Groups.RemoveFromGroupAsync(connection, sessionId);
                }

                return true;
            }
        }

        public async Task joinSessionAsSubscriber(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            await Clients.OthersInGroup(sessionId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {sessionId}.");
            activeSessions[sessionId].Add(Context.ConnectionId);
        }
        
        public async Task joinSessionAsPublisher(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            await Clients.OthersInGroup(sessionId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {sessionId}.");
            activeSessions[sessionId].Add(Context.ConnectionId);
        }
        
        public async Task leaveSession(string sessionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);
            await Clients.OthersInGroup(sessionId).SendAsync("Send", $"{Context.ConnectionId} has left the group {sessionId}.");
            activeSessions[sessionId].Remove(Context.ConnectionId);
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