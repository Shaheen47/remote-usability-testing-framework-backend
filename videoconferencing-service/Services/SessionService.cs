using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using videoconferencing_service.Proxies.OpenVidu;

namespace videoconferencing_service.Services
{
    public class SessionService : ISessionService
    {
        private readonly IConfiguration Configuration;
        
        // OpenVidu object as entrypoint of the SDK
        private OpenViduProxy openVidu;
        private string openViduUrl;
        private string openViduSecret;



        public SessionService(IConfiguration configuration)
        {
            Configuration = configuration;
            openVidu = new OpenViduProxy(configuration);
            openViduUrl = configuration["OpenVidu:OPENVIDU_URL"];
            openViduSecret = configuration["OpenVidu:SECRET"];

        }

        

        public async Task<string> createSession()
        {
            //generate random sessionName
            var session = await openVidu.createSession();
            return session.sessionId;
        }
        
        public async Task closeSession(string sessionId)
        {
            await openVidu.endSession(sessionId);
        }

        public async Task<String> joinSessionAsModerator(string sessionId)
        {
            Session session=await openVidu.getSession(sessionId);
            Connection connection= await session.createConnection(OpenViduRole.MODERATOR,openViduUrl,openViduSecret);
            return connection.Token;
        }

        public async Task<String>  joinSessionAsObserver(string sessionId)
        {
            Session session=await openVidu.getSession(sessionId);
            Connection connection= await session.createConnection(OpenViduRole.SUBSCRIBER,openViduUrl,openViduSecret);
            return connection.Token;
        }

        public async Task<String>  joinSessionAsParticipant(string sessionId)
        {
            Session session=await openVidu.getSession(sessionId);
            Connection connection= await session.createConnection(OpenViduRole.PUBLISHER,openViduUrl,openViduSecret);
            return connection.Token;
        }

        public async Task removeUserFromSession(string sessionName,String connectionId)
        {
            var session = await openVidu.getSession(sessionName);
            await session.deleteConnection(connectionId,openViduUrl,openViduSecret);
        }
        
    }
}