using System.Collections.Generic;

namespace session_service.Entities
{
    public class Moderator
    {
        
        public string id { set; get; }
        public string name{ set; get; }
        public string username { set; get; }
        public string password { set; get; }
        public List<Session> sessions { get; set; }
        
        public Moderator()
        {
            sessions = new List<Session>();
        }
    }
}