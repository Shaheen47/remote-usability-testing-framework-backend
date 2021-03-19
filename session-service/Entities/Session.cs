using System.Collections.Generic;


namespace session_service.Entities
{
    public  class Session
    {
        
        // session service 


        public Session(string chatId)
        {
            this.chatId = chatId;
            observerstConferencingTokens = new List<string>();
        }

        
        public string id { set; get; }
        //chat
        public string chatId { get; set; }
        public string chatHubUrl { get; set; }

        
        //screensharing service
        public string screenSharingSessionId { get; set; }
        public string screenSharingHubUrl { get; set; }
        /*public string screenSharingGroupId { get; set; }*/
        
        //videoConferencing service
        public string videoConferencingSessionId { get; set; }
        public string moderatorConferenceToken { get; set; }
        public string participantConferenceToken { get; set; }
        public IList<string> observerstConferencingTokens { get; set; }
        
    }
}