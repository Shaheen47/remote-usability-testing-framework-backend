using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using videoconferencing_service.Proxies.OpenVidu;

namespace videoconferencing_service.Services
{
    public class SessionService : ISessionService
    {
        private readonly IConfiguration Configuration;
        
        private IConferenceProviderProxy conferenceProviderProxy;
        private string openViduUrl;
        private string openViduSecret;
        private Dictionary<string, Session> activeSessions;



        public SessionService(IConfiguration configuration,IConferenceProviderProxy conferenceProviderProxy)
        {
            this.Configuration = configuration;
            this.conferenceProviderProxy = conferenceProviderProxy;
            openViduUrl = configuration["OpenVidu:OPENVIDU_URL"];
            openViduSecret = configuration["OpenVidu:SECRET"];
            activeSessions = new Dictionary<string, Session>();

        }

        

        public async Task<string> createSession()
        {
            //generate random sessionName
            var session = await conferenceProviderProxy.createSession();
            activeSessions.Add(session.sessionId,session);
            return session.sessionId;
        }
        
        public async Task closeSession(string sessionId)
        {
            await conferenceProviderProxy.endSession(sessionId);
            activeSessions.Remove(sessionId);
        }

        public async Task<String> joinSessionAsModerator(string sessionId)
        {
            Session session=activeSessions[sessionId];
            /*Session session=await openVidu.getSession(sessionId);*/
            Connection connection= await session.createConnection(OpenViduRole.MODERATOR,openViduUrl,openViduSecret);
            return connection.Token;
        }

        public async Task<String>  joinSessionAsObserver(string sessionId)
        {
            Session session=activeSessions[sessionId];
            /*Session session=await openVidu.getSession(sessionId);*/
            Connection connection= await session.createConnection(OpenViduRole.SUBSCRIBER,openViduUrl,openViduSecret);
            return connection.Token;
        }

        public async Task<String>  joinSessionAsParticipant(string sessionId)
        {
            Session session=activeSessions[sessionId];
            /*Session session=await openVidu.getSession(sessionId);*/
            Connection connection= await session.createConnection(OpenViduRole.PUBLISHER,openViduUrl,openViduSecret);
            return connection.Token;
        }

        public async Task removeUserFromSession(string sessionId,String connectionId)
        {
            Session session=activeSessions[sessionId];
            /*Session session=await openVidu.getSession(sessionId);*/
            await session.deleteConnection(connectionId,openViduUrl,openViduSecret);
        }
        
    }
}