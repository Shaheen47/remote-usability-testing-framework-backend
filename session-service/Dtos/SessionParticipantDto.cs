namespace session_service
{
    public class SessionParticipantDto
    {
        public string id { set; get; }
        
        //chat service
        public string chatSessionId { get; set; }
        public string chatHubUrl { get; set; }

        //screensharing service
        public string screenSharingSessionId { get; set; }
        public string screenSharingHubUrl { get; set; }
        /*public string screenSharingGroupId { get; set; }*/
        
        //videoConferencing service
        public string videoConferencingSessionId { get; set; }
        public string participantConferenceToken { get; set; }
    }
}