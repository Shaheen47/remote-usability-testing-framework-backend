using System;
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
    public class ScreenEventsReplyService: IScreenEventsReplyService
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
        
        public ScreenEventsReplyService(IScreenMirroringRepository screenMirroringRepository,IHubContext<ScreenMirroringHub> hubContext)
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
     
                Console.WriteLine("time:"+eventsToSend[0].timestamp);
   
                foreach (ScreenMirroringEvent screenMirroringEvent in eventsToSend)
                {
                    if (screenMirroringEvent.GetType() == typeof(DomInitializationEvent))
                        hubContext.Clients.Group(sessionId).SendAsync("domInitialization",((DomInitializationEvent)screenMirroringEvent).content);
                    else if (screenMirroringEvent.GetType() == typeof(DomChangeEvent))
                        hubContext.Clients.Group(sessionId).SendAsync("domChanges",((DomChangeEvent)screenMirroringEvent).content);
                    else if (screenMirroringEvent.GetType() == typeof(ClearDomEvent))
                        hubContext.Clients.Group(sessionId).SendAsync("clearDom",((ClearDomEvent)screenMirroringEvent));
                    else if (screenMirroringEvent.GetType() == typeof(BaseUrlChangedEvent))
                        hubContext.Clients.Group(sessionId).SendAsync("baseUrlChanged",((BaseUrlChangedEvent)screenMirroringEvent).url);
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
            Console.WriteLine("stopwatch.Count"+stopwatch.Count);
            /*var x = await screenMirroringRepository.getAllEvents(sessionId, EventType.dom);
            var y = await screenMirroringRepository.getAllEvents(sessionId, EventType.inputChanged);
            var z = await screenMirroringRepository.getAllEventsStartingFrom(sessionId,10000);
            var zx = await screenMirroringRepository.getAllEventsStartingFrom(sessionId,13000);*/
            isReplying[sessionId] = false;
            /*isReplying.Add(sessionId, false);*/
            elapsedTime[sessionId] = 0;
            /*elapsedTime.Add(sessionId,0);*/
            addedTime[sessionId] = 0;
            /*addedTime.Add(sessionId,0);*/
            mirroringEventsDic[sessionId] =
                await screenMirroringRepository.getAllEvents(sessionId);
            var events=await screenMirroringRepository.getAllEvents(sessionId);
            /*mirroringEventsDic.Add(sessionId,events);*/
            var x=await screenMirroringRepository.getAllEvents(sessionId, EventType.domInitilization);
            Console.WriteLine("initizlia events from db=" +x.ToList().Count); ;
            mirroringEventsDic[sessionId]=events;
            stopwatch[sessionId] = new Stopwatch();
            /*stopwatch.Add(sessionId,new Stopwatch());*/

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
            
        }

        public void stopSessionReply(string sessionId)
        {
            isReplying[sessionId] = false;
            stopwatch[sessionId].Stop();
        }
    }
}