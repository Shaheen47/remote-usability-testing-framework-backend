using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace session_service.Entities
{
    [Table("session")]
    public  class Session
    {
        
        // session service 


        public Session(int chatId, int moderatorId)
        {
            this.chatId = chatId;
            this.moderatorId = moderatorId;
        }

        public int id { set; get; }
        public int chatId { get; set; }
        public int moderatorId { set; get; }
        
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