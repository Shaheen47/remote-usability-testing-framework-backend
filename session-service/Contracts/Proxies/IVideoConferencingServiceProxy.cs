using System.Threading.Tasks;

namespace session_service.Contracts.Proxies
{
    public interface IVideoConferencingServiceProxy
    {
        public Task<string> createSession();
        
        public Task stopSession(string sessionId);

        public Task<string> joinAsModerator(string sessionId);
        
        public Task<string> joinAsParticipant(string sessionId);
        
        public Task<string> joinAsObserver(string sessionId);

        public Task startRecording(string sessionId);
        
        public Task stopRecording(string sessionId);
        
    }
}