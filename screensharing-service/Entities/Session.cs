namespace screensharing_service.Entities
{
    public class Session
    {
        public string sessionId { get; set; }
        public string hubUrl { get; set; }
        
        public string replyHubUrl { get; set; }
        
        public string replyControllingHubUrl { get; set; }
    }
}