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
        private Dictionary<string, bool> isReplying;
        // the running time
        private Dictionary<string,Stopwatch>  stopwatch;
        // all events occured before this time is already sent
        private Dictionary<string, long> elapsedTime;
        // we need some sort of added time to deal with replyFromTimestamp
        private Dictionary<string, long> addedTime;
        
        public ScreenEventsReplyService(IScreenMirroringRepository screenMirroringRepository,IHubContext<ScreenMirroringHub> hubContext)
        {
            this.screenMirroringRepository = screenMirroringRepository;
            this.hubContext =hubContext;
            this.stopwatch = new Dictionary<string, Stopwatch>();
            this.mirroringEventsDic = new Dictionary<string, IEnumerable<ScreenMirroringEvent>>();
            this.isReplying = new Dictionary<string, bool>();
            this.elapsedTime = new Dictionary<string, long>();
            addedTime = new Dictionary<string, long>();
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
                    else if(screenMirroringEvent.GetType() == typeof(MousePosition))
                        hubContext.Clients.Group(sessionId).SendAsync("sentMousePosition",((MousePosition)screenMirroringEvent).left,((MousePosition)screenMirroringEvent).top);
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
                    else
                        hubContext.Clients.Group(sessionId).SendAsync("sentScroll",((ScrollPosition)screenMirroringEvent).vertical);
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
            /*isReplying[sessionId] = false;*/
            isReplying.Add(sessionId, false);
            /*elapsedTime[sessionId] = 0;*/
            elapsedTime.Add(sessionId,0);
            /*addedTime[sessionId] = 0;*/
            addedTime.Add(sessionId,0);
            /*mirroringEventsDic[sessionId] =
                await screenMirroringRepository.getAllEvents(sessionId);*/
            Console.WriteLine("Reading events"+stopwatch.Count);
            var events=await screenMirroringRepository.getAllEvents(sessionId);
            Console.WriteLine("events.Count"+events.Count());
            mirroringEventsDic.Add(sessionId,events);
            /*stopwatch[sessionId] = new Stopwatch();*/
            stopwatch.Add(sessionId,new Stopwatch());
            Console.WriteLine("stopwatch.Count"+stopwatch.Count);

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