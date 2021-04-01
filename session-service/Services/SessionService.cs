using System.Threading.Tasks;
using AutoMapper;
using session_service.Contracts.Proxies;
using session_service.Contracts.Repositories;
using session_service.Contracts.Services;
using session_service.Core.Exceptions;
using session_service.Dtos;
using session_service.Entities;
using session_service.Proxies;

namespace session_service.Services
{
    public class SessionService : ISessionService
    {
        private ISessionRepository sessionRepository;
        private IChatRepository chatRepository;
        private IVideoConferencingServiceProxy conferencingServiceProxy;
        private IScreensharingServiceProxy screensharingServiceProxy;
        
        private readonly IMapper Mapper;

        private const string chatHubBaseUrl = "https://localhost:5001/";
        /*private const string chatHubBaseUrl = "https://18.185.136.179/";*/

        public SessionService(ISessionRepository sessionRepository, IChatRepository chatRepository, IVideoConferencingServiceProxy conferencingServiceProxy,IScreensharingServiceProxy screensharingServiceProxy, IMapper Mapper)
        {
            this.sessionRepository = sessionRepository;
            this.chatRepository = chatRepository;
            this.conferencingServiceProxy = conferencingServiceProxy;
            this.screensharingServiceProxy = screensharingServiceProxy;
            this.Mapper = Mapper;
        }

        public async Task<SessionCreationResponseDto> createSession()
        {
            
            Session session = new Session();
            session.isRecorded = false;
            //create chat session
            Chat chat=chatRepository.createChat(new Chat());
            session.chatSessionId = chat.id;
            session.chatHubUrl =chatHubBaseUrl+ "ChatHub";

            //create videoconference session by communicating with the VideoConferencingService
            session.videoConferencingSessionId=await conferencingServiceProxy.createSession();
            
            //create screensharing session by communicating with the ScreensharingService
            var screensharingSession=await screensharingServiceProxy.createSession();
            session.screenSharingHubUrl = screensharingSession.hubUrl;
            session.screenSharingSessionId = screensharingSession.sessionId;
            
            // store 
            session=await sessionRepository.Create(session);
            await sessionRepository.Save();

            //return 
            var sessionCreationDto=Mapper.Map<Session, SessionCreationResponseDto>(session);
            return sessionCreationDto;
            
        }

        
        public async Task<SessionCreationResponseDto> createSessionWithRecording()
        {
            Session session = new Session();
            session.isRecorded = true;
            //create chat session
            Chat chat=chatRepository.createChat(new Chat());
            session.chatSessionId = chat.id;
            session.chatHubUrl = chatHubBaseUrl+"ChatHubWithRecording";
            
            //create videoconference session by communicating with the VideoConferencingService
            session.videoConferencingSessionId=await conferencingServiceProxy.createSession();
            
            //create screensharing session by communicating with the ScreensharingService
            RecordedScreensharingSession screensharingSession=await screensharingServiceProxy.createSessionWithRecording();
            session.screenSharingHubUrl = screensharingSession.hubUrl;
            session.screenSharingSessionId = screensharingSession.sessionId;
            session.screenSharingReplyHubUrl = screensharingSession.replyHubUrl;
            session.screenSharingReplyControllingHubUrl = screensharingSession.replyControllingHubUrl;
            
            // store 
            session=await sessionRepository.Create(session);
            await sessionRepository.Save();

            //return 
            var sessionCreationDto=Mapper.Map<Session, SessionCreationResponseDto>(session);
            return sessionCreationDto;
            
        }

        
        public async Task stopSession(Session session)
        {
            
            //stop session and chat
            
            
            
            //stop video recording and get the url
            if(session.isRecorded) 
                session.videoRecordingUrl=await conferencingServiceProxy.stopRecording(session.videoConferencingSessionId);

            //stop video session
            conferencingServiceProxy.stopSession(session.videoConferencingSessionId);
            
            //stop screensharing session
            screensharingServiceProxy.stopSession(session.screenSharingSessionId);
            

            
        }

        public async Task<SessionModeratorDto> joinAsModerator(string sessionId, string observerName)
        {
            Session session=await sessionRepository.FindById(sessionId);
            
            //check whether the moderator is already joined
            if (session.moderatorConferenceToken != null)
                throw new ModeratorAlreadyJoinedExecption();
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
            
            //check whether the moderator is already joined
            if (session.participantConferenceToken != null)
                throw new ParticipantAlreadyJoinedExecption();
            
            //call conferencingServiceProxy to create conference connection for the participant
            session.participantConferenceToken = await conferencingServiceProxy.joinAsParticipant(session.videoConferencingSessionId);
            
            //check whether to start recording or not
            if (session.isRecorded == true)
                await conferencingServiceProxy.startRecording(session.videoConferencingSessionId);
            
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
            sessionObserverDto.observerConferencingToken = token;
            return sessionObserverDto;
        }

        
        public async Task<Session> getSession(string sessionId)
        {
            Session session=await sessionRepository.FindById(sessionId);
            return session;
        }
        

        public string getRecordingUrl(Session session)
        {
            throw new System.NotImplementedException();
        }

        public void replyScreensharing(Session session)
        {
            screensharingServiceProxy.replySession(session.screenSharingSessionId);
        }
        
    }
}