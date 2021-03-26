using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using screensharing_service.Contracts.Services;

namespace screensharing_service.Hubs
{
    public class ScreenMirroringReplyControllingHub:Hub
    {
        private IScreenEventsReplyService replyService;
        
        public ScreenMirroringReplyControllingHub(IScreenEventsReplyService replyService)
        {
            this.replyService = replyService;
        }
        
        
        public async Task  startReply(string sessionId)
        {
             replyService.startSessionReply(sessionId);
             await Clients.All.SendAsync("test", "startReply done");
        }

        public async Task pauseReply(string sessionId)
        {
             replyService.pauseSessionReply(sessionId);
             await Clients.All.SendAsync("test", "pauseSessionReply done");
        }
        
        public async Task continueReply(string sessionId)
        {
             replyService.continueSessionReply(sessionId);
             await Clients.All.SendAsync("test", "continueSessionReply done");
        }
        
        public async Task seekReply(string sessionId,long timestamp)
        {
            replyService.replyFromTimestamp(sessionId,timestamp);
            await Clients.All.SendAsync("test", "seekReply done");
        }
        
        public async Task stopReply(string sessionId)
        {
            replyService.stopSessionReply(sessionId);
        }
    }
}
