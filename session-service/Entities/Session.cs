using System.Collections.Generic;


namespace session_service.Entities
{
    public  class Session
    {
        
        // session service 


        public Session()
        {
            observersConferencingTokens = new List<string>();
        }

        
        public string id { set; get; }
        
        public bool isRecorded { set; get; }
        //chat
        public string chatSessionId { get; set; }
        public string chatHubUrl { get; set; }

        
        //screensharing service
        public string screenSharingSessionId { get; set; }
        public string screenSharingHubUrl { get; set; }
        public string screenSharingReplyHubUrl { get; set; }
        
        public string screenSharingReplyControllingHubUrl { get; set; }
        /*public string screenSharingGroupId { get; set; }*/
        
        //videoConferencing service
        public string videoConferencingSessionId { get; set; }
        public string moderatorConferenceToken { get; set; }
        public string participantConferenceToken { get; set; }
        public IList<string> observersConferencingTokens { get; set; }
        
        public string videoRecordingUrl { get; set; }

        public SessionStatus status { get; set; }

    }
}