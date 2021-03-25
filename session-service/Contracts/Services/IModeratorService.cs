using System.Threading.Tasks;
using session_service.Dtos;
using session_service.Entities;

namespace session_service.Contracts.Services
{
    public interface IModeratorService
    {
        public Task<Moderator> createModerator(ModeratorCreationDto moderatorDto);
        
        public Task<Moderator> getModerator(string moderatorId);
        
        public Task<Moderator> updateModerator(Moderator moderator);

        public bool loginModerator(ModeratorLoginDto moderatorLoginDto);


    }
}