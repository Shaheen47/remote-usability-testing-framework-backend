using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Contracts.Services;
using screensharing_service.Dtos;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Services
{
    public class ScreenEventsRecordingService : IScreenEventsRecordingService
    {
        
        private IScreenMirroringRepository screenMirroringRepository;
        private Dictionary<string, Stopwatch> stopWatches;
        
        public ScreenEventsRecordingService(IScreenMirroringRepository domRepository)
        {
            this.screenMirroringRepository = domRepository;
            stopWatches = new Dictionary<string, Stopwatch>();
        }
        

        public void startSession(string sessionId)
        {
            screenMirroringRepository.CreateSession(sessionId);
            stopWatches.Add(sessionId,new Stopwatch());
            stopWatches[sessionId].Start();
        }

        public void stopSession(string sessionId)
        {
            stopWatches[sessionId].Stop();
            stopWatches.Remove(sessionId);
        }

        public async Task AddDomInitializationEvent(string sessionId, string content)
        {
            DomInitializationEvent domInitializationEvent = new DomInitializationEvent
            {
                content = content,
                timestamp = stopWatches[sessionId].ElapsedMilliseconds
            };
            await screenMirroringRepository.addEvent(domInitializationEvent,sessionId);
            //Console.WriteLine("Dom init added,it has bytes:"+System.Text.ASCIIEncoding.Unicode.GetByteCount(content));
        }

        public void AddDomChangeEvent(string sessionId, string content)
        {
            DomChangeEvent domChangeEvent = new DomChangeEvent
            {
                content = content,
                timestamp = stopWatches[sessionId].ElapsedMilliseconds
            };
            screenMirroringRepository.addEvent(domChangeEvent,sessionId);
        }

        public void AddDomClearEvent(string sessionId)
        {
            ClearDomEvent clearDomEvent = new ClearDomEvent
            {
                timestamp = stopWatches[sessionId].ElapsedMilliseconds
            };
            
            screenMirroringRepository.addEvent(clearDomEvent,sessionId);
        }

        public void AddBaseUrlChangedEvent(string sessionId, string url)
        {
            BaseUrlChangedEvent baseUrlChangedEvent = new BaseUrlChangedEvent
            {
                url = url,
                timestamp = stopWatches[sessionId].ElapsedMilliseconds
            };
            screenMirroringRepository.addEvent(baseUrlChangedEvent,sessionId);
        }


        public void addMouseUpEvent(string sessionId)
        {
            MouseUpEvent mouseUpEvent = new MouseUpEvent {timestamp = stopWatches[sessionId].ElapsedMilliseconds};
            screenMirroringRepository.addEvent(mouseUpEvent,sessionId);
        }

        public void addMouseDownEvent(string sessionId)
        {
            MouseDownEvent mouseDownEvent = new MouseDownEvent {timestamp = stopWatches[sessionId].ElapsedMilliseconds};
            screenMirroringRepository.addEvent(mouseDownEvent,sessionId);
        }

        public void addMouseOverEvent(string sessionId, string elementXpath)
        {
            MouseOverEvent mouseOverEvent = new MouseOverEvent()
            {
                timestamp = stopWatches[sessionId].ElapsedMilliseconds,
                elementXpath = elementXpath
            };
            screenMirroringRepository.addEvent(mouseOverEvent,sessionId);
        }

        public void addMouseOutEvent(string sessionId, string elementXpath)
        {
            MouseOutEvent mouseOutEvent = new MouseOutEvent()
            {
                timestamp = stopWatches[sessionId].ElapsedMilliseconds,
                elementXpath = elementXpath
            };
            screenMirroringRepository.addEvent(mouseOutEvent,sessionId);
        }

        public void addInputChangedEvent(string sessionId, string elementXpath, string inputContent)
        {
            InputChangedEvent inputChangedEvent = new InputChangedEvent()
            {
                timestamp = stopWatches[sessionId].ElapsedMilliseconds,
                elementXpath = elementXpath,
                content = inputContent
            };
            screenMirroringRepository.addEvent(inputChangedEvent,sessionId);
        }
        
    }
}