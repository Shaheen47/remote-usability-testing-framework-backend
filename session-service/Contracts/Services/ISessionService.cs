using System.Threading.Tasks;
using session_service.Entities;

namespace session_service.Contracts.Services
{
    public interface ISessionService
    {

        public Task<SessionCreationDto> createSession();
        
        public Task<SessionModeratorDto> joinAsModerator(string sessionId,string moderatorName);

        public Task<SessionParticipantDto> joinAsParticipant(string sessionId,string participantName);

        public Task<SessionObserverDto> joinAsObserver(string sessionId,string observerName);
        
        public Task<Session> getSession(string sessionId);
        
        
        public void stopSession(Session session);
        
        public void startRecording(Session session);
        
        public void stopRecording(Session session);
        
        public string getRecordingUrl(Session session);
    }
}