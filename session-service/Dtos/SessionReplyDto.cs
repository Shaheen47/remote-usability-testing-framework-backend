namespace session_service
{
    public class SessionReplyDto
    {
        public string id { set; get; }
        
        
        //screensharing service
        public string screenSharingSessionId { get; set; }
        public string screenSharingHubUrl { get; set; }

        //videoConferencing service
        public string videoRecordingUrl { get; set; }
    }
}