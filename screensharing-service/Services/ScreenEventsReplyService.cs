using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Contracts.Services;
using screensharing_service.Entities.ScreenMirroring;
using screensharing_service.Hubs;

namespace screensharing_service.Services
{
    public class ScreenEventsReplyService:IScreenEventsReplyService
    {
        
        private IScreenMirroringRepository screenMirroringRepository;
        private IHubContext<ScreenMirroringHub> hubContext;
        
        
        public ScreenEventsReplyService(IScreenMirroringRepository screenMirroringRepository,IHubContext<ScreenMirroringHub> hubContext)
        {
            this.screenMirroringRepository = screenMirroringRepository;
            this.hubContext =hubContext;
        }

        
        public void startSessionReply(string sessionId)
        {
            var mirroringEvents = screenMirroringRepository.GetAllScreenMirroringEventsSortedByTimestamp(sessionId); 
            Stopwatch stopwatch=Stopwatch.StartNew();
            long lasttime= 0;
            long sentEvents = 0;
            while (sentEvents<(mirroringEvents.Count))
            {
                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                var eventsToSend = mirroringEvents.Where(x => (x.timestamp <= elapsedMilliseconds) && (x.timestamp > lasttime))
                    .OrderBy(p=>p.timestamp).ToList();
                foreach (ScreenMirroringEvent screenMirroringEvent in eventsToSend)
                {
                    if (screenMirroringEvent.GetType() == typeof(DomEvent))
                            hubContext.Clients.All.SendAsync("sentDom",((DomEvent)screenMirroringEvent).content);
                    else if(screenMirroringEvent.GetType() == typeof(MousePosition))
                            hubContext.Clients.All.SendAsync("sentMousePosition",((MousePosition)screenMirroringEvent).left,((MousePosition)screenMirroringEvent).top);
                    else
                            hubContext.Clients.All.SendAsync("sentScroll",((ScrollPosition)screenMirroringEvent).vertical);
                    sentEvents++;
                }

                lasttime = elapsedMilliseconds;
                System.Threading.Thread.Sleep(17);
                
            }

        }

        public void stopSessionReply(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public void pauseSessionReply(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public void continueSessionReply(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public void replyFromTimestamp(string sessionId, long timestamp)
        {
            throw new System.NotImplementedException();
        }
    }
}