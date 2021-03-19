using System.Threading.Tasks;
using AutoMapper;
using session_service.Contracts.Proxies;
using session_service.Contracts.Repositories;
using session_service.Contracts.Services;
using session_service.Entities;

namespace session_service.Services
{
    public class SessionService : ISessionService
    {
        private ISessionRepository sessionRepository;
        private IChatRepository chatRepository;
        private IVideoConferencingServiceProxy conferencingServiceProxy;
        private IScreensharingServiceProxy screensharingServiceProxy;
        
        private readonly IMapper Mapper;

        public SessionService(ISessionRepository sessionRepository, IChatRepository chatRepository, IVideoConferencingServiceProxy conferencingServiceProxy,IScreensharingServiceProxy screensharingServiceProxy, IMapper Mapper)
        {
            this.sessionRepository = sessionRepository;
            this.chatRepository = chatRepository;
            this.conferencingServiceProxy = conferencingServiceProxy;
            this.screensharingServiceProxy = screensharingServiceProxy;
            this.Mapper = Mapper;
        }

        public async Task<Session> createSession()
        {
            
            Session session = new Session();
            
            //create chat session
            Chat chat=await chatRepository.Create(new Chat());
            session.chatSessionId = chat.id;
            session.chatHubUrl = "https://localhost:5001/Session/ChatHub";
            
            //create videoconference session by communicating with the VideoConferencingService
            session.videoConferencingSessionId=await conferencingServiceProxy.createSession();
            
            //create screensharing session by communicating with the ScreensharingService
            var screensharingSession=await screensharingServiceProxy.createSession();
            session.screenSharingHubUrl = screensharingSession.hubUrl;
            session.screenSharingSessionId = screensharingSession.sessionId;
            
            // store 
            session=await sessionRepository.Create(session);
            await sessionRepository.Save();
            await chatRepository.Save();
            
            //return 
            return session;
            
        }
        
        public void stopSession(Session session)
        {
            
            //stop session and chat
            
            
            //stop videoconference
            
            
            //stop screensharing 

            
            throw new System.NotImplementedException();
        }

        public async Task<SessionModeratorDto> joinAsModerator(string sessionId, string observerName)
        {
            Session session=await sessionRepository.FindById(sessionId);
            
            //call conferencingServiceProxy to create conference connection for the moderator
            session.moderatorConferenceToken=await conferencingServiceProxy.joinAsModerator(session.videoConferencingSessionId);
            
            //save
            await sessionRepository.Update(session);
            await sessionRepository.Save();
            
            var sessionModeratorDto=Mapper.Map<Session, SessionModeratorDto>(session);
            return sessionModeratorDto;
        }

        public async Task<SessionParticipantDto> joinAsParticipant(string sessionId, string observerName)
        {
            Session session=await sessionRepository.FindById(sessionId);
            
            //call conferencingServiceProxy to create conference connection for the participant
            session.participantConferenceToken = await conferencingServiceProxy.joinAsParticipant(session.videoConferencingSessionId);
            
            //save
            await sessionRepository.Update(session);
            await sessionRepository.Save();
            
            var sessionParticipantDto=Mapper.Map<Session, SessionParticipantDto>(session);
            return sessionParticipantDto;
        }
        
        public async Task<SessionObserverDto> joinAsObserver(string sessionId, string observerName)
        {
            
            Session session=await sessionRepository.FindById(sessionId);
            //call conferencingServiceProxy to create conference connection for the observer
            var token=await conferencingServiceProxy.joinAsObserver(session.videoConferencingSessionId);
            
            session.observersConferencingTokens.Add(token);
            
            await sessionRepository.Update(session);
            await sessionRepository.Save();
            var sessionObserverDto=Mapper.Map<Session, SessionObserverDto>(session);
            sessionObserverDto.observerstConferencingToken = token;
            return sessionObserverDto;
        }

        
        public async Task<Session> getSession(string sessionId)
        {
            Session session=await sessionRepository.FindById(sessionId);
            return session;
        }
        
        

        public void startRecording(Session session)
        {
            
            //start videoconference recording
            
            
            //start screensharing  recording
            
            throw new System.NotImplementedException();
        }

        public void stopRecording(Session session)
        {
            
            //stop videoconference recording
            
            
            //stop screensharing  recording
            
            throw new System.NotImplementedException();
        }

        public string getRecording(Session session)
        {
            throw new System.NotImplementedException();
        }
    }
}