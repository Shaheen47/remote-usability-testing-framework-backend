using screensharing_service.Entities;

namespace screensharing_service.Contracts.Repositories
{
    public interface ISessionRepository
    {
        public Session createSession(Session entity);

        public Session getSession(string sessionId);
    }
}