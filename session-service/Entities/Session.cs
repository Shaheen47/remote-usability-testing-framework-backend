using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace session_service.Entities
{
    public  class Session
    {
        
        // session service 


        public Session(int chatId)
        {
            this.chatId = chatId;
            observerstConferencingTokens = new List<string>();
        }

        public int id { set; get; }
        public int chatId { get; set; }

        //screensharing service
        public string screenSharingSessionId { get; set; }
        public string screenSharingHubUrl { get; set; }
        
        //videoConferencing service
        public string videoConferencingSessionId { get; set; }
        public string moderatorConferenceToken { get; set; }
        public string participantConferenceToken { get; set; }
        public IList<string> observerstConferencingTokens { get; set; }
        
    }
}