namespace session_service
{
    public class SessionObserverDto
    {
        
        public string id { set; get; }
        
        //chat service
        public string chatSessionId { get; set; }
        public string chatHubUrl { get; set; }

        //screensharing service
        public string screenSharingSessionId { get; set; }
        public string screenSharingHubUrl { get; set; }

        //videoConferencing service
        public string observerstConferencingToken { get; set; }
    }
}