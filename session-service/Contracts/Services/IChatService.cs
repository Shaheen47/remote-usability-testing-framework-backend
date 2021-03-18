using System.Collections.Generic;
using System.Threading.Tasks;
using session_service.Entities;

namespace session_service.Contracts.Services
{
    public interface IChatService
    {

        public Task addMessage(int chatId, ChatMessage message);

        public Task<List<ChatMessage>> GetChatMessages(int chatId);
    }
}