using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using session_service.Contracts.Repositories;
using session_service.Core;
using session_service.Entities;

namespace session_service.Repositories
{
    
    public class DumbSessionRepo : ISessionRepository
    {

        private IList<Session> sessions;


        public DumbSessionRepo()
        {
            sessions = new List<Session>();
            Session session1 = new Session();
            session1.isRecorded = true;
            session1.status = SessionStatus.FINISHED;
            session1.id = "dsadad";
            session1.sessionDate=DateTime.Now.ToLocalTime();
            Session session2= new Session();
            session2.isRecorded = true;
            session2.status = SessionStatus.FINISHED;
            session2.id = "fdsfsdf";
            session2.sessionDate=DateTime.Now;
            Session session3= new Session();
            session3.isRecorded = false;
            session3.status = SessionStatus.FINISHED;
            session3.id = "dadaw";
            session3.sessionDate=DateTime.Now;
            sessions.Add(session1);     
            sessions.Add(session2);     
            sessions.Add(session3);     

        }

        public async Task<IList<Session>> FindAll()
        {
            return sessions;
        }

        public async Task<Session> FindById(string id)
        {
            foreach (var session in sessions)
            {
                if (session.id == id)
                    return session;
            }
            return null;
        }

        public async Task<Session> Create(Session entity)
        {
            Session session = entity;
            session.id=RandomKeyGenerator.GetUniqueKey(10);
            sessions.Add(entity);
            return session;
        }

        public async Task<bool> Save()
        {
            return true;
        }

        public async Task<bool> Update(Session entity)
        {
            return true;
        }

        public async Task<bool> Delete(Session entity)
        {
            return true;
        }

        public async Task<IList<Session>> findAllRecordedSessions()
        {
            return sessions.Where(session => session.isRecorded  && session.status==SessionStatus.FINISHED).ToList();
        }
    }
}