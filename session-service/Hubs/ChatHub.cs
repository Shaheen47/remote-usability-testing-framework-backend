using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using session_service.Contracts;
using session_service.Contracts.Services;
using session_service.Entities;

namespace session_service.Hubs
{
    public class ChatHub : Hub
    {
        public ISessionService sessionService;
        private IChatService chatService;
        public async Task sendMessage(int sessionId,string senderName, string message)
        {
            await Clients.All.SendAsync("chatMessageSent", senderName, message);

            Session session=await sessionService.getSession(sessionId);
            ChatMessage chatMessage = new ChatMessage(senderName,message,DateTime.Now);
            await chatService.addMessage(session.chatId,chatMessage);
        }
    }
}