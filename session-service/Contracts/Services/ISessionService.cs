using session_service.Entities;

namespace session_service.Contracts
{
    public interface ISessionService
    {

        public Session createSession(Moderator moderator);

        public void startSession(Session session);
        
        public void stopSession(Session session);
        
        public void startRecording(Session session);
        
        public void stopRecording(Session session);
        
        public string getRecording(Session session);
    }
}