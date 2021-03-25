using System.Threading.Tasks;
using session_service.Contracts;
using session_service.Contracts.Repositories;
using session_service.Contracts.Services;
using session_service.Dtos;
using session_service.Entities;

namespace session_service.Services
{
    public class ModeratorService : IModeratorService
    {
        private IModeratorRepository moderatorRepository;
        public Task<Moderator> createModerator(ModeratorCreationDto moderatorDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<Moderator> getModerator(string moderatorId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Moderator> updateModerator(Moderator moderator)
        {
            throw new System.NotImplementedException();
        }

        public bool loginModerator(ModeratorLoginDto moderatorLoginDto)
        {
            throw new System.NotImplementedException();
        }
    }
}