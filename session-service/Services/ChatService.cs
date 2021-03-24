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

        public ChatService(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public  void addChat(Chat chat)
        {
             chatRepository.createChat(chat);
        }

        public  void addMessage(string chatId, ChatMessage message)
        {
            chatRepository.addChatMessage(chatId,message);
        }

        public IList<ChatMessage> GetChatMessages(string chatId)
        {
            var messages= chatRepository.getChatMessages(chatId);
            return messages;
        }
    }
}