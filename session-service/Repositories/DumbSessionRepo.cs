using System;
using System.Collections.Generic;
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
    }
}