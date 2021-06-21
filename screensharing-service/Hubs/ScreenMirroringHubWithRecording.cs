using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using screensharing_service.Contracts.Services;
using screensharing_service.Dtos;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Hubs
{
    public class ScreenMirroringHubWithRecording: Hub,IScreenMirroringHub
    {
        private IScreenEventsRecordingService screenEventsRecordingService;

        public ScreenMirroringHubWithRecording(IScreenEventsRecordingService screenEventsRecordingService)
        {
            this.screenEventsRecordingService = screenEventsRecordingService;
        }
        

        public async Task closeSession(string sessionId)
        {
            await Clients.Group(sessionId).SendAsync("leaveSession");
        }

        public async Task joinSessionAsSubscriber(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            //
            await Clients.OthersInGroup(sessionId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {sessionId}.");
        }
        
        public async Task joinSessionAsPublisher(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            //
            await Clients.OthersInGroup(sessionId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {sessionId}.");
            screenEventsRecordingService.startSession(sessionId);
        }
        
        public async Task leaveSession(string sessionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);

            await Clients.OthersInGroup(sessionId).SendAsync("Send", $"{Context.ConnectionId} has left the group {sessionId}.");
        }

        public async Task sendDomInitialization(string sessionId, string initialDom,string baseUrl)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("domInitialization", initialDom,baseUrl);
            screenEventsRecordingService.AddDomInitializationEvent(sessionId,initialDom,baseUrl);
            Console.WriteLine("adding initial dom to database");
        }

        public async Task sendDomChanges(string sessionId, string domChanges)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("domChanges", domChanges);
            screenEventsRecordingService.AddDomChangeEvent(sessionId,domChanges);
        }

        public async Task sendClearDom(string sessionId)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("clearDom");
            screenEventsRecordingService.AddDomClearEvent(sessionId);
        }


        public async Task mouseUp(string sessionId)
        {
             await Clients.OthersInGroup(sessionId).SendAsync("mouseUp");
             screenEventsRecordingService.addMouseUpEvent(sessionId);
        }

        public async Task mouseDown(string sessionId)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("mouseDown");
            screenEventsRecordingService.addMouseDownEvent(sessionId);
        }

        public async Task mouseOver(string sessionId, string elementXpath)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("mouseOver",elementXpath);
            screenEventsRecordingService.addMouseOverEvent(sessionId,elementXpath);
        }

        public async Task mouseOut(string sessionId, string elementXpath)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("mouseOut",elementXpath);
            screenEventsRecordingService.addMouseOverEvent(sessionId,elementXpath);
        }
        

        public async Task inputChanged(string sessionId, string elementXpath, string inputContent)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("inputChanged",elementXpath,inputContent);
            screenEventsRecordingService.addInputChangedEvent(sessionId,elementXpath,inputContent);
        }
    }
}