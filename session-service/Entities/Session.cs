using System.Collections.Generic;

namespace session_service.Entities
{
    public class Session
    {
        
        // session service 
        public int id { set; get; }
        public Chat chat { get; set; }
        
        //screensharing service
        public string screenSharingSessionId { get; set; }
        public string screenSharingHubUrl { get; set; }
        
        //videoConferencing service
        public string videoConferencingSessionId { get; set; }
        public string videoConferencingServerUrl { get; set; }
        public string moderatorConferencingToken { get; set; }
        public string participantConferencingToken { get; set; }
        public List<string> observerstConferencingTokens { get; set; }
        
    }
}