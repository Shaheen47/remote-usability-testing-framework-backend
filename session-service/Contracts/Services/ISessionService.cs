using System.Threading.Tasks;
using session_service.Dtos;
using session_service.Entities;

namespace session_service.Contracts.Services
{
    public interface ISessionService
    {

        public Task<SessionCreationResponseDto> createSession();
        
        public Task<SessionCreationResponseDto> createSessionWithRecording();
        
        public Task<SessionModeratorDto> joinAsModerator(string sessionId,string moderatorName);

        public Task<SessionParticipantDto> joinAsParticipant(string sessionId,string participantName);

        public Task<SessionObserverDto> joinAsObserver(string sessionId,string observerName);
        
        public Task<Session> getSession(string sessionId);
        
        
        public Task stopSession(Session session);
        
        public void replyScreensharing(Session session);
    }
}