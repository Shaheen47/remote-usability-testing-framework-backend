using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using videoconferencing_service.Contracts.Repositories;
using videoconferencing_service.Contracts.Services;
using videoconferencing_service.Proxies.OpenVidu;

namespace videoconferencing_service.Services
{
    public class SessionService : ISessionService
    {
        private readonly IConfiguration Configuration;

        private ISessionRepository sessionRepository;
        private IConferenceProviderProxy conferenceProviderProxy;
        private string openViduUrl;
        private string openViduSecret;
       



        public SessionService(IConfiguration configuration,IConferenceProviderProxy conferenceProviderProxy,ISessionRepository sessionRepository)
        {
            this.Configuration = configuration;
            this.conferenceProviderProxy = conferenceProviderProxy;
            this.sessionRepository = sessionRepository;
            openViduUrl = configuration["OpenVidu:OPENVIDU_URL"];
            openViduSecret = configuration["OpenVidu:SECRET"];

        }

        

        public async Task<string> createSession()
        {
            //generate random sessionName
            var session = await conferenceProviderProxy.createSession();
            sessionRepository.addSession(session);
            return session.sessionId;
        }
        
        public async Task closeSession(string sessionId)
        {
            await conferenceProviderProxy.endSession(sessionId);
            /*sessionRepository.removeSession(sessionId);*/
            //we need to deal with both active sessions and saved sessions information
        }

        public async Task<String> joinSessionAsModerator(string sessionId)
        {
            Session session=sessionRepository.getSession(sessionId);
            /*Session session=await openVidu.getSession(sessionId);*/
            Connection connection= await session.createConnection(OpenViduRole.MODERATOR,openViduUrl,openViduSecret);
            return connection.Token;
        }

        public async Task<String>  joinSessionAsObserver(string sessionId)
        {
            Session session=sessionRepository.getSession(sessionId);
            /*Session session=await openVidu.getSession(sessionId);*/
            Connection connection= await session.createConnection(OpenViduRole.SUBSCRIBER,openViduUrl,openViduSecret);
            return connection.Token;
        }

        public async Task<String>  joinSessionAsParticipant(string sessionId)
        {
            Session session=sessionRepository.getSession(sessionId);
            /*Session session=await openVidu.getSession(sessionId);*/
            Connection connection= await session.createConnection(OpenViduRole.PUBLISHER,openViduUrl,openViduSecret);
            return connection.Token;
        }

        public async Task removeUserFromSession(string sessionId,String connectionId)
        {
            Session session=sessionRepository.getSession(sessionId);
            /*Session session=await openVidu.getSession(sessionId);*/
            await session.deleteConnection(connectionId,openViduUrl,openViduSecret);
        }
        
    }
}