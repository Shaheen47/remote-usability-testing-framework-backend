using System.Diagnostics;
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
        private IHubContext<DomHub> hubContext;
        
        
        public ScreenEventsReplyService(IScreenMirroringRepository screenMirroringRepository,IHubContext<DomHub> hubContext)
        {
            this.screenMirroringRepository = screenMirroringRepository;
            this.hubContext =hubContext;
        }

        
        public void startSessionReply(string sessionId)
        {
            var mirroringEvents = screenMirroringRepository.GetAllScreenMirroringEvents(sessionId);
            Stopwatch stopwatch=Stopwatch.StartNew();
            long lasttime= 0;
            while (mirroringEvents.Count>0)
            {
                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                foreach (ScreenMirroringEvent screenMirroringEvent in mirroringEvents)
                {
                    if (screenMirroringEvent.timestamp <= elapsedMilliseconds&&screenMirroringEvent.timestamp>lasttime )
                    {
                        if (screenMirroringEvent.GetType() == typeof(DomEvent))
                            hubContext.Clients.All.SendAsync("sentDom",((DomEvent)screenMirroringEvent).content);
                        else if(screenMirroringEvent.GetType() == typeof(MousePosition))
                            hubContext.Clients.All.SendAsync("sentMousePosition",((MousePosition)screenMirroringEvent).left,((MousePosition)screenMirroringEvent).top);
                        else
                            hubContext.Clients.All.SendAsync("sentScroll",((ScrollPosition)screenMirroringEvent).vertical);

                        /*mirroringEvents.Remove(screenMirroringEvent);*/
                    }
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
    }
}