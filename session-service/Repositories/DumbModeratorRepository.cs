using System.Collections.Generic;
using System.Threading.Tasks;
using session_service.Contracts.Repositories;
using session_service.Entities;

namespace session_service.Repositories
{
    public class DumbModeratorRepository: IModeratorRepository
    {
        public Task<IList<Moderator>> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Moderator> FindById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Moderator> Create(Moderator entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(Moderator entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(Moderator entity)
        {
            throw new System.NotImplementedException();
        }
    }
}