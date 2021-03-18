using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using session_service.Contracts.Repositories;
using session_service.Entities;

namespace session_service.Repositories
{
    public class DumbChatRepo : IChatRepository
    {
        private IList<Chat> chats;

        public DumbChatRepo()
        {
            chats = new List<Chat>();
        }

        public async Task<IList<Chat>> FindAll()
        {
            return chats;
        }

        public async Task<Chat> FindById(int id)
        {
            foreach (var chat in chats)
            {
                if (chat.id == id)
                    return chat;
            }
            return null;
        }

        public async Task<Chat> Create(Chat entity)
        {
            Chat chat = entity;
            var random = new Random();
            chat.id=random.Next();
            chats.Add(entity);
            return chat;
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
    }
}