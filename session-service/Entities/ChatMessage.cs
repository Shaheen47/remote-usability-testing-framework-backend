using System;

namespace session_service.Entities
{
    public class ChatMessage
    {
        
        public User sender { get; set; }
        
        public string message { get; set; }
        
        public DateTime timestamp { get; set; }
        
    }
}