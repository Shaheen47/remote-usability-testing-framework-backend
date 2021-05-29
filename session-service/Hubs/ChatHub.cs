using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace session_service.Hubs
{
    public class ChatHub: Hub,IChatHub
    {
        private ConcurrentDictionary<string, IList<string>> activeSessions;

        public ChatHub()
        {
            activeSessions = new ConcurrentDictionary<string, IList<string>>();
        }

        public bool createSession(string chatSessionId)
        {
            if (activeSessions.ContainsKey(chatSessionId))
                return false;
            else
            {
                activeSessions.TryAdd(chatSessionId, new List<string>());
                return true;
            }
            
        }

        public async Task joinSession(string chatSessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatSessionId);
            await Clients.Group(chatSessionId).SendAsync("userJoined", $"{Context.ConnectionId} has joined the group {chatSessionId}.");
            activeSessions[chatSessionId].Add(Context.ConnectionId);
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

        public bool deleteSession(string chatSessionId)
        {
            if (activeSessions.ContainsKey(chatSessionId))
                return false;
            else
            {
                activeSessions.TryRemove(chatSessionId, out var sessionConnections);
                foreach (string connection in sessionConnections)
                {
                    Groups.RemoveFromGroupAsync(connection, chatSessionId);
                }

                return true;
            }
        }
    }
}