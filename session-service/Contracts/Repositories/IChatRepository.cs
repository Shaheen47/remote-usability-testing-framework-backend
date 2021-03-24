using System.Collections.Generic;
using System.Threading.Tasks;
using session_service.Entities;

namespace session_service.Contracts.Repositories
{
    public interface IChatRepository
    {
        public Chat createChat(Chat chat);
        public void addChatMessage(string chatId, ChatMessage message);

        public IList<ChatMessage> getChatMessages(string chatId);
    }
}