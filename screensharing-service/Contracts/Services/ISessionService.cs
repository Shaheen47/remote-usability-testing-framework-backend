using System.Threading.Tasks;
using screensharing_service.Entities;

namespace screensharing_service.Contracts.Services
{
    public interface ISessionService
    {
        public Task<Session> createSession();

        public void closeSession(string sessionId);
        
        public Task<Session> createSessionWithRecording();
        
    }
}