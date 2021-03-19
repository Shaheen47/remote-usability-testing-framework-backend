using System.Threading.Tasks;
using session_service.Entities;

namespace session_service.Contracts.Services
{
    public interface ISessionService
    {

        public Task<Session> createSession();

        public Task<Session> getSession(string sessionId);

        public void startSession(Session session);
        
        public void stopSession(Session session);
        
        public void startRecording(Session session);
        
        public void stopRecording(Session session);
        
        public string getRecording(Session session);
    }
}