using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using session_service.Contracts.Services;
using session_service.Entities;

namespace session_service.Hubs
{
    public class ChatHubWithRecording : Hub,IChatHub
    {
        private IChatService chatService;

        public ChatHubWithRecording(IChatService chatService)
        {
            this.chatService = chatService;
        }

        
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
        
        public async Task sendMessage(string chatSessionId,string senderName, string message)
        {
            await Clients.Group(chatSessionId).SendAsync("messageSent", senderName, message);
            
            ChatMessage chatMessage = new ChatMessage(senderName,message,DateTime.Now);
            chatService.addMessage(chatSessionId,chatMessage);
        }

        public async Task closeSession(string chatSessionId)
        {
            await Clients.Group(chatSessionId).SendAsync("leaveSession");
        }
    }
}