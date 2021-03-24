using System.Threading.Tasks;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Contracts.Services;
using screensharing_service.Entities;

namespace screensharing_service.Services
{
    public class SessionService: ISessionService
    {
        private ISessionRepository sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public async Task<Session> createSession()
        {
            Session session = new Session();
            session.hubUrl = "https://localhost:5005/DomHub";
            var createdSession=sessionRepository.createSession(session);
            return createdSession;
        }
    }
}