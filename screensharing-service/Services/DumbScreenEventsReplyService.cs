using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Contracts.Services;
using screensharing_service.Entities.ScreenMirroring;
using screensharing_service.Hubs;

namespace screensharing_service.Services
{
    public class DumbScreenEventsReplyService:IScreenEventsReplyService
    {
        
        private IScreenMirroringRepository screenMirroringRepository;
        private IHubContext<ScreenMirroringHub> hubContext;
        private Dictionary<string, IEnumerable<ScreenMirroringEvent>> mirroringEventsDic;
        private ConcurrentDictionary<string, bool> isReplying;
        // the running time
        private ConcurrentDictionary<string,Stopwatch>  stopwatch;
        // all events occured before this time is already sent
        private ConcurrentDictionary<string, long> elapsedTime;
        // we need some sort of added time to deal with replyFromTimestamp
        private ConcurrentDictionary<string, long> addedTime;
        
        
        public DumbScreenEventsReplyService(IScreenMirroringRepository screenMirroringRepository,IHubContext<ScreenMirroringHub> hubContext)
        {
            this.screenMirroringRepository = screenMirroringRepository;
            this.hubContext =hubContext;
            this.stopwatch = new ConcurrentDictionary<string, Stopwatch>();
            this.mirroringEventsDic = new Dictionary<string, IEnumerable<ScreenMirroringEvent>>();
            this.isReplying = new ConcurrentDictionary<string, bool>();
            this.elapsedTime = new ConcurrentDictionary<string, long>();
            addedTime = new ConcurrentDictionary<string, long>();
        }

        
        
        private  void performMirroring(string sessionId)
        {
            isReplying[sessionId] = true;
            while (isReplying[sessionId])
            {
                var elapsedMilliseconds = stopwatch[sessionId].ElapsedMilliseconds + addedTime[sessionId];
                var eventsToSend = mirroringEventsDic[sessionId].Where(x => (x.timestamp <= elapsedMilliseconds) && (x.timestamp > elapsedTime[sessionId]))
                    .OrderBy(p=>p.timestamp).ToList();
                foreach (ScreenMirroringEvent screenMirroringEvent in eventsToSend)
                {
                    if (screenMirroringEvent.GetType() == typeof(DomEvent))
                          hubContext.Clients.Group(sessionId).SendAsync("sentDom",((DomEvent)screenMirroringEvent).content);
                    else if (screenMirroringEvent.GetType() == typeof(MouseUpEvent))
                        hubContext.Clients.Group(sessionId).SendAsync("mouseUp");
                    else if (screenMirroringEvent.GetType() == typeof(MouseDownEvent))
                        hubContext.Clients.Group(sessionId).SendAsync("mouseDown");
                    else if (screenMirroringEvent.GetType() == typeof(MouseOverEvent))
                        hubContext.Clients.Group(sessionId).SendAsync("mouseOver",((MouseOverEvent)screenMirroringEvent).elementXpath);
                    else if (screenMirroringEvent.GetType() == typeof(MouseOutEvent))
                        hubContext.Clients.Group(sessionId).SendAsync("mouseOut",((MouseOutEvent)screenMirroringEvent).elementXpath);
                    else if (screenMirroringEvent.GetType() == typeof(InputChangedEvent))
                        hubContext.Clients.Group(sessionId).SendAsync("inputChanged",((InputChangedEvent)screenMirroringEvent).elementXpath,((InputChangedEvent)screenMirroringEvent).content);
                }

                elapsedTime[sessionId] = elapsedMilliseconds;
                // send 60 fps
                System.Threading.Thread.Sleep(17);
            }
        }


        public async Task startSessionReply(string sessionId)
        {
            isReplying[sessionId] = false;
            elapsedTime[sessionId] = 0;
            addedTime[sessionId] = 0;
            mirroringEventsDic[sessionId] =
                await screenMirroringRepository.getAllEvents(sessionId);
            stopwatch[sessionId] = new Stopwatch();

        }



        public void  pauseSessionReply(string sessionId)
        {
            isReplying[sessionId] = false;
            stopwatch[sessionId].Stop();
        }

        public async Task continueSessionReply(string sessionId)
        {
            stopwatch[sessionId].Start();
            await Task.Run(() => performMirroring(sessionId));
        }

        public async Task replyFromTimestamp(string sessionId, long timestamp)
        {
            isReplying[sessionId] = false;
            
            //send clear page
            await hubContext.Clients.Group(sessionId).SendAsync("sentDom","{'clear':'clear'}");
            
            // deal with timers
            addedTime[sessionId] = timestamp;
            stopwatch[sessionId].Restart();
            elapsedTime[sessionId] = 0;
            
            
            // basic solution for now




            //much better solution

            // send just the latest mouse position and scroll position

            // send clear page


            // send all dom events from beginning or from the last initialize
        }

        public void stopSessionReply(string sessionId)
        {
            isReplying[sessionId] = false;
            stopwatch[sessionId].Stop();
            //more logic
        }
    
    }
}