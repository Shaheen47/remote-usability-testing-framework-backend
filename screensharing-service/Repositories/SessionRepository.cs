using System.Collections.Generic;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Entities;
using session_service.Core;

namespace screensharing_service.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private IList<Session> sessions;

        public SessionRepository()
        {
            sessions = new List<Session>();
        }

        public Session createSession(Session entity)
        {
            Session session = entity;
            session.sessionId=RandomKeyGenerator.GetUniqueKey(10);
            sessions.Add(entity);
            return session;
        }

        public Session getSession(string sessionId)
        {
            foreach (var session in sessions)
            {
                if (session.sessionId == sessionId)
                    return session;
            }
            return null;
        }
    }
}