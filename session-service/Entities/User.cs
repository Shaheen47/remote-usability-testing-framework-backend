using System.Collections.Generic;

namespace session_service.Entities
{
    public abstract class User
    {
        
        
        public string firstName { set; get; }
        public string lastName { set; get; }
        public string email { set; get; }
        
    }
}