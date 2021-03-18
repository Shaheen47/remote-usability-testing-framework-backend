using System.Collections.Generic;

namespace session_service.Entities
{
    public class Moderator: User
    {
        public int id { set; get; }
        public List<Session> sessions { get; set; }
    }
}