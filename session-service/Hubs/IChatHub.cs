using System.Threading.Tasks;

namespace session_service.Hubs
{
    public interface IChatHub
    {


        public Task joinSession(string chatSessionId);

        public Task leaveSession(string chatSessionId);

        public Task sendMessage(string chatSessionId, string senderName, string message);

        public Task closeSession(string chatSessionId);
    }
}