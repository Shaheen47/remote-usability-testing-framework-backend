using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using screensharing_service.Contracts.Services;
using screensharing_service.Dtos;

namespace screensharing_service.Hubs
{
    public class DomHub : Hub
    {

        private IScreenEventsRecordingService screenEventsRecordingService;

        public DomHub(IScreenEventsRecordingService screenEventsRecordingService)
        {
            this.screenEventsRecordingService = screenEventsRecordingService;
        }

        public async Task joinSession(string sessionId)
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
        
        
        public async Task sendMousePosition(string sessionId,int x,int y)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("sentMousePosition", x,y);
            screenEventsRecordingService.addMousemovementEvent(x,y,sessionId);
        }
        
        public async Task sendScroll(string sessionId,int vertical)
        {
            await Clients.OthersInGroup(sessionId).SendAsync("sentScroll",vertical);
            screenEventsRecordingService.addScrollingEvent(vertical,sessionId);
        }
        
    }
}