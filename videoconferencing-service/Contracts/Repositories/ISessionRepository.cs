using videoconferencing_service.Proxies.OpenVidu;

namespace videoconferencing_service.Contracts.Repositories
{
    public interface ISessionRepository
    {
        public void addSession(Session session);

        public Session getSession(string sessionId);
        
        public void removeSession(string sessionId);
    }
}