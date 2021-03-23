using screensharing_service.Dtos;
using screensharing_service.Entities;

namespace screensharing_service.Contracts.Services
{
    public interface IScreenMirroringEventsService
    {
        public void AddDomEvent(DomEventCreationDto domEventCreationDto,string sessionId);

        public void addMousemovementEvent(int x, int y,string sessionId);

        public void addScrollingEvent(int vertical, string sessionId);
    }
}