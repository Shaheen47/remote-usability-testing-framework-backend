using System.Threading.Tasks;

namespace screensharing_service.Contracts.Services
{
    public interface ISessionService
    {
        public Task<string> generateScreenSharingHub();
        
    }
}