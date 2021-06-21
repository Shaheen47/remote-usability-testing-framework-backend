using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
             Console.WriteLine("startReply started");
        }

        public async Task pauseReply(string sessionId)
        {
             replyService.pauseSessionReply(sessionId);
        }
        
        public async Task continueReply(string sessionId)
        {
             replyService.continueSessionReply(sessionId);
        }
        
        public async Task seekReply(string sessionId,long timestamp)
        {
            replyService.replyFromTimestamp(sessionId,timestamp);
        }
        
        public async Task stopReply(string sessionId)
        {
            replyService.stopSessionReply(sessionId);
        }
    }
}
