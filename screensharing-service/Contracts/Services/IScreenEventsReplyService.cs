using System.Threading.Tasks;

namespace screensharing_service.Contracts.Services
{
    public interface IScreenEventsReplyService
    {
        public Task startSessionReply(string sessionId);
        public void stopSessionReply(string sessionId);
        public  void pauseSessionReply(string sessionId);
        public  Task continueSessionReply(string sessionId);
        
        public Task replyFromTimestamp(string sessionId, long timestamp);
    }
}