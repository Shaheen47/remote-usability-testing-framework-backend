using System.Collections.Generic;
using System.Threading.Tasks;
using session_service.Entities;


namespace session_service.Contracts.Repositories

    
{
    public interface ISessionRepository : IRepositoryBase<Session>
    {
        Task<IList<Session>> findAllRecordedSessions();
    }
}