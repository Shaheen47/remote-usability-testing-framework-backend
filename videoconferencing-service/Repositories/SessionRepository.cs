using System.Collections.Generic;
using videoconferencing_service.Contracts.Repositories;
using videoconferencing_service.Proxies.OpenVidu;

namespace videoconferencing_service.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private Dictionary<string, Session> activeSessions;
        
        public SessionRepository()
        {
            activeSessions = new Dictionary<string, Session>();
        }
        
        
        public void addSession(Session session)
        {
            activeSessions.Add(session.sessionId,session);
        }

        public Session getSession(string sessionId)
        {
            return activeSessions[sessionId];
        }

        public void removeSession(string sessionId)
        {
            activeSessions.Remove(sessionId);
        }
    }
}