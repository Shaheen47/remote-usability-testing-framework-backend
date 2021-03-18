using System.Threading.Tasks;
using session_service.Contracts;
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
        
        public async Task<Session> createSession()
        {
            //create session and chat
            Chat chat=await chatRepository.Create(new Chat());
            Session session = new Session( chat.id);
            
            //create videoconference
            session.videoConferencingSessionId=await conferencingServiceProxy.createSession();
            session.moderatorConferenceToken =
                await conferencingServiceProxy.joinAsModerator(session.videoConferencingSessionId);
            session.participantConferenceToken =
                await conferencingServiceProxy.joinAsParticipant(session.videoConferencingSessionId);
            //create screensharing
            
            
            // store and return 
            session=await sessionRepository.Create(session);

            await sessionRepository.Save();
            await chatRepository.Save();
            
            return session;
            
        }
        

        public async Task<Session> getSession(int sessionId)
        {
            Session session=await sessionRepository.FindById(sessionId);
            return session;
        }

        public void startSession(Session session)
        {
            
            //start session and chat
            
            
            //start videoconference
            
            
            //start screensharing 
            
            throw new System.NotImplementedException();
        }

        public void stopSession(Session session)
        {
            
            //stop session and chat
            
            
            //stop videoconference
            
            
            //stop screensharing 

            
            throw new System.NotImplementedException();
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