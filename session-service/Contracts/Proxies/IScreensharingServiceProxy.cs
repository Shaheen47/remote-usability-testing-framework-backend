using System.Threading.Tasks;
using session_service.Proxies;

namespace session_service.Contracts.Proxies
{
    public interface IScreensharingServiceProxy
    {
        public Task<ScreensharingSession> createSession();
        
        public Task<ScreensharingSession> createSessionWithRecording();
        
        public Task stopSession(string sessionId);
        

        public Task replySession(string sessionId);
    }
}