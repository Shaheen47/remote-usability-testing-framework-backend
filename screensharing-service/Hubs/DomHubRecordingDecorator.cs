using System.Threading.Tasks;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Contracts.Services;
using screensharing_service.Dtos;

namespace screensharing_service.Hubs
{
    public class DomHubRecordingDecorator: IDomHub
    {
        
        private IDomHub wrappee;
        private IScreenMirroringEventsService screenMirroringEventsService;
        
        public DomHubRecordingDecorator(IDomHub wrappee,IScreenMirroringEventsService screenMirroringEventsService)
        {
            this.wrappee = wrappee;
            this.screenMirroringEventsService = screenMirroringEventsService;
        }
        
        public Task joinSession(string sessionId)
        {
            throw new System.NotImplementedException();
            
        }

        public Task leaveSession(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public async Task sendDom(string sessionId, string dom)
        {
            await wrappee.sendDom(sessionId, dom);
            var domEventCreationDto = new DomEventCreationDto(dom);
            screenMirroringEventsService.AddDomEvent(domEventCreationDto,sessionId);
        }

        public async Task sendMousePosition(string sessionId, int x, int y)
        {
            await wrappee.sendMousePosition(sessionId,x,y);
            screenMirroringEventsService.addMousemovementEvent(x,y,sessionId);
        }

        public async Task sendScroll(string sessionId, int vertical)
        {
            await wrappee.sendScroll(sessionId, vertical);
            screenMirroringEventsService.addScrollingEvent(vertical,sessionId);
        }
    }
}