using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using screensharing_service.Contracts.Services;
using screensharing_service.Dtos;

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

        public async Task sendDom(string sessionId,string dom)
        {
            
            await Clients.OthersInGroup(sessionId).SendAsync("sentDom", dom);
            var domEventCreationDto = new DomEventCreationDto(dom);
            screenEventsRecordingService.AddDomEvent(domEventCreationDto,sessionId);
        }
        
        
        public async Task sendMousePosition(string sessionId,float x,float y)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("sentMousePosition", x,y);
            screenEventsRecordingService.addMousemovementEvent(x,y,sessionId);
        }
        
        public async Task sendScroll(string sessionId,int vertical)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("sentScroll",vertical);
            screenEventsRecordingService.addScrollingEvent(vertical,sessionId);
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

        public async Task urlParameterChange(string sessionId, string queryString)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("urlParameterChange",queryString);
            screenEventsRecordingService.addUrlParameterChangedEvent(sessionId,queryString);
        }

        public async Task inputChanged(string sessionId, string elementXpath, string inputContent)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("inputChanged",elementXpath,inputContent);
            screenEventsRecordingService.addInputChangedEvent(sessionId,elementXpath,inputContent);
        }
    }
}