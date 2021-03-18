using System;

namespace session_service.Entities
{
    public class ChatMessage
    {
        public ChatMessage(string sender, string message, DateTime timestamp)
        {
            this.sender = sender;
            this.message = message;
            this.timestamp = timestamp;
        }

        public string sender { get; set; }
        
        public string message { get; set; }
        
        public DateTime timestamp { get; set; }
        
    }
}