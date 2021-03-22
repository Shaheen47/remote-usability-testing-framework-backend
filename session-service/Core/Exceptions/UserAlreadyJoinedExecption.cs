using System;

namespace session_service.Core.Exceptions
{
    public class ModeratorAlreadyJoinedExecption : Exception
    {
        public ModeratorAlreadyJoinedExecption() : base("only one Moderator can join the session")
        {
            
        }
    }
    
    public class ParticipantAlreadyJoinedExecption : Exception
    {
        public ParticipantAlreadyJoinedExecption() : base("only one Participant can join the session ")
        {
            
        }
    }
}