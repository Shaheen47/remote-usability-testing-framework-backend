using System.Collections.Generic;
using System.Threading.Tasks;
using session_service.Contracts;
using session_service.Contracts.Repositories;
using session_service.Contracts.Services;
using session_service.Entities;

namespace session_service.Services
{
    public class ChatService : IChatService
    {
        private IChatRepository chatRepository;
        
        public async Task addMessage(int chatId, ChatMessage message)
        {
            Chat chat= await chatRepository.FindById(chatId);
            chat.messages.Add(message);
            await chatRepository.Update(chat);
        }

        public async Task<List<ChatMessage>> GetChatMessages(int chatId)
        {
            Chat chat= await chatRepository.FindById(chatId);
            return chat.messages;
        }
    }
}