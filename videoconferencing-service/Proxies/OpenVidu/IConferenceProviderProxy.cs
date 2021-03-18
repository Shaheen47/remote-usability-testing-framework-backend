using System.Threading.Tasks;

namespace videoconferencing_service.Proxies.OpenVidu
{
    public interface IConferenceProviderProxy
    {
        public Task<Session> createSession();

        public Session getSession(string sessionId);

        public Task endSession(string sessionId);

        public  Task<Session> refreshSessionInfo(string sessionId);

    }
}