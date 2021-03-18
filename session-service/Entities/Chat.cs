using System.Collections.Generic;

namespace session_service.Entities
{
    public class Chat
    {
        public int id { get; set; }
        public List<ChatMessage> messages { get; set; }
        
    }
}