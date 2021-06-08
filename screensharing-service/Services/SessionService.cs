using System.Threading.Tasks;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Contracts.Services;
using screensharing_service.Entities;

namespace screensharing_service.Services
{
    public class SessionService: ISessionService
    {
        private ISessionRepository sessionRepository;
        
        private const string hubBaseUrl="https://localhost:5005/";
        //private const hubBaseUrl="https://18.184.14.204:5005/";

        public SessionService(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public async Task<Session> createSession()
        {
            Session session = new Session();
            session.hubUrl =hubBaseUrl+ "ScreenMirroringHub";
            var createdSession=sessionRepository.createSession(session);
            return createdSession;
        }

        public void closeSession(string sessionId)
        {
            //delete from active sessions
            /*throw new System.NotImplementedException();*/
            
        }

        public async Task<Session> createSessionWithRecording()
        {
            Session session = new Session();
            session.hubUrl =hubBaseUrl+ "ScreenMirroringHubWithRecording";
            session.replyHubUrl =hubBaseUrl+ "ScreenMirroringHub";
            session.replyControllingHubUrl =hubBaseUrl+ "ScreenMirroringReplyControllingHub";
            var createdSession=sessionRepository.createSession(session);
            return createdSession;
        }
    }
}