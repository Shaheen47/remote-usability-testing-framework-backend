using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            screenMirroringRepository.addSession(sessionId);
            stopWatches.Add(sessionId,new Stopwatch());
            stopWatches[sessionId].Start();
        }

        public void stopSession(string sessionId)
        {
            stopWatches[sessionId].Stop();
            stopWatches.Remove(sessionId);
        }

        public void AddDomEvent(DomEventCreationDto domEventCreationDto, string sessionId)
        {
            DomEvent domEvent = new DomEvent();
            domEvent.id = "dsad";
            domEvent.content = domEventCreationDto.content;
            domEvent.timestamp = stopWatches[sessionId].ElapsedMilliseconds;
            screenMirroringRepository.addDomEvent(domEvent,sessionId);
        }

        public void addMousemovementEvent(int x, int y, string sessionId)
        {
            MousePosition mousePosition = new MousePosition();
            mousePosition.id = "dsad";
            mousePosition.left = x;
            mousePosition.top = y;
            mousePosition.timestamp = stopWatches[sessionId].ElapsedMilliseconds;
            screenMirroringRepository.addMouseMovementEvent(mousePosition,sessionId);
        }

        public void addScrollingEvent(int vertical, string sessionId)
        {
            ScrollPosition scrollPosition = new ScrollPosition();
            scrollPosition.id = "dsad";
            scrollPosition.vertical = vertical;
            scrollPosition.timestamp = stopWatches[sessionId].ElapsedMilliseconds;
            screenMirroringRepository.addScrollingEvent(scrollPosition,sessionId);
        }
    }
}