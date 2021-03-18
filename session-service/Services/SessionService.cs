using session_service.Contracts;
using session_service.Entities;

namespace session_service.Services
{
    public class SessionService : ISessionService
    {
        public Session createSession(Moderator moderator)
        {
            
            //create session and chat
            
            
            //create videoconference
            
            //create screensharing
            
            
            throw new System.NotImplementedException();
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