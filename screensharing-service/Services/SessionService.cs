using System.Threading.Tasks;
using screensharing_service.Contracts.Services;
using screensharing_service.Entities;

namespace screensharing_service.Services
{
    public class SessionService: ISessionService
    {
        public async Task<Session> createSession()
        {
            Session session = new Session();
            session.hubUrl = "https://localhost:5005/DomHub";
            session.sessionId = "dsasT23F";
            return session;
        }
    }
}