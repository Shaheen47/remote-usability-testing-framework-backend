using System.Collections.Generic;
using System.Threading.Tasks;
using session_service.Entities;

namespace session_service.Contracts.Services
{
    public interface IChatService
    {

        public void addChat(Chat chat);
        public void addMessage(string chatId, ChatMessage message);

        public IList<ChatMessage> GetChatMessages(string chatId);
        
    }
}