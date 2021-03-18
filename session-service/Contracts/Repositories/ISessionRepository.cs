using session_service.Entities;
using System.Collections.Generic;  
using Microsoft.EntityFrameworkCore;

namespace session_service.Contracts.Repositories

    
{
    public interface ISessionRepository : IRepositoryBase<Session>
    {
        
    }
}