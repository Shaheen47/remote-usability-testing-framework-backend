using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using session_service.Contracts.Repositories;
using session_service.Core;
using session_service.Entities;

namespace session_service.Repositories
{
    public class DumbChatRepo : IChatRepository
    {
        private Dictionary<string,IList<ChatMessage> > chatMessages;

        public DumbChatRepo()
        {
            chatMessages = new Dictionary<string, IList<ChatMessage>>();
        }
        
        
        

        public async Task<bool> Save()
        {
            return true;
        }

        public async Task<bool> Update(Chat entity)
        {
            return true;
        }

        public async Task<bool> Delete(Chat entity)
        {
            return true;
        }

        public Chat createChat(Chat chat)
        {
            Chat chat1 = chat;
            chat1.id = RandomKeyGenerator.GetUniqueKey(10);
            chatMessages.Add(chat1.id, new List<ChatMessage>());
            return chat1;
        }
        

        public  void addChatMessage(string chatId, ChatMessage message)
        {
              chatMessages[chatId].Add(message);
        }

        public IList<ChatMessage> getChatMessages(string chatId)
        {
            return chatMessages[chatId];
        }
    }
}