using System;
using System.Threading.Tasks;

namespace videoconferencing_service.Services
{
    public interface ISessionService
    {

        public Task<string> createSession();
        
        
        public Task<String> joinSessionAsModerator(string sessionName);
        
        public Task<String> joinSessionAsObserver(String sessionName);
        
        public Task<String> joinSessionAsParticipant(String sessionName);
        

        public Task removeUserFromSession(String sessionName,String connectionId);

        public Task closeSession(string sessionName);
    }
}