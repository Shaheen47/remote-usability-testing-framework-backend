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
            //screenMirroringRepository.CreateSession(sessionId);
            stopWatches.Add(sessionId,new Stopwatch());
            stopWatches[sessionId].Start();
        }

        public void stopSession(string sessionId)
        {
            stopWatches[sessionId].Stop();
            stopWatches.Remove(sessionId);
        }

        public  void AddDomInitializationEvent(string sessionId, string content,string baseUrl)
        {
            DomInitializationEvent domInitializationEvent = new DomInitializationEvent
            {
                content = content,
                baseUrl = baseUrl,
                timestamp = stopWatches[sessionId].ElapsedMilliseconds,
                sessionId = sessionId
            };
             screenMirroringRepository.addEvent(domInitializationEvent);
            //Console.WriteLine("Dom init added,it has bytes:"+System.Text.ASCIIEncoding.Unicode.GetByteCount(content));
        }

        public void AddDomChangeEvent(string sessionId, string content)
        {
            DomChangeEvent domChangeEvent = new DomChangeEvent
            {
                content = content,
                timestamp = stopWatches[sessionId].ElapsedMilliseconds,
                sessionId = sessionId
            };
            screenMirroringRepository.addEvent(domChangeEvent);
        }

        public void AddDomClearEvent(string sessionId)
        {
            ClearDomEvent clearDomEvent = new ClearDomEvent
            {
                timestamp = stopWatches[sessionId].ElapsedMilliseconds,
                sessionId = sessionId
            };
            
            screenMirroringRepository.addEvent(clearDomEvent);
        }
        


        public void addMouseUpEvent(string sessionId)
        {
            MouseUpEvent mouseUpEvent = new MouseUpEvent
            {
                timestamp = stopWatches[sessionId].ElapsedMilliseconds,
                sessionId = sessionId
            };
            screenMirroringRepository.addEvent(mouseUpEvent);
        }

        public void addMouseDownEvent(string sessionId)
        {
            MouseDownEvent mouseDownEvent = new MouseDownEvent
            {
                timestamp = stopWatches[sessionId].ElapsedMilliseconds,
                sessionId = sessionId
            };
            screenMirroringRepository.addEvent(mouseDownEvent);
        }

        public void addMouseOverEvent(string sessionId, string elementXpath)
        {
            MouseOverEvent mouseOverEvent = new MouseOverEvent()
            {
                timestamp = stopWatches[sessionId].ElapsedMilliseconds,
                elementXpath = elementXpath,
                sessionId = sessionId
            };
            screenMirroringRepository.addEvent(mouseOverEvent);
        }

        public void addMouseOutEvent(string sessionId, string elementXpath)
        {
            MouseOutEvent mouseOutEvent = new MouseOutEvent()
            {
                timestamp = stopWatches[sessionId].ElapsedMilliseconds,
                elementXpath = elementXpath,
                sessionId = sessionId
            };
            screenMirroringRepository.addEvent(mouseOutEvent);
        }

        public void addInputChangedEvent(string sessionId, string elementXpath, string inputContent)
        {
            InputChangedEvent inputChangedEvent = new InputChangedEvent()
            {
                timestamp = stopWatches[sessionId].ElapsedMilliseconds,
                elementXpath = elementXpath,
                content = inputContent,
                sessionId = sessionId
            };
            screenMirroringRepository.addEvent(inputChangedEvent);
        }
        
    }
}