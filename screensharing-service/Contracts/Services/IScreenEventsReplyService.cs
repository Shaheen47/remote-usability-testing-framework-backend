namespace screensharing_service.Contracts.Services
{
    public interface IScreenEventsReplyService
    {
        public void startSessionReply(string sessionId);
        public void stopSessionReply(string sessionId);
        public void pauseSessionReply(string sessionId);
        public void continueSessionReply(string sessionId);
        
        public void replyFromTimestamp(string sessionId,long timestamp);
    }
}