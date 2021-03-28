using screensharing_service.Dtos;
using screensharing_service.Entities;

namespace screensharing_service.Contracts.Services
{
    public interface IScreenEventsRecordingService
    {

        public void startSession(string sessionId);

        public void stopSession(string sessionId);
        public void AddDomEvent(DomEventCreationDto domEventCreationDto,string sessionId);

        public void addMousemovementEvent(int x, int y,string sessionId);

        public void addScrollingEvent(int vertical, string sessionId);

        public void addMouseUpEvent(string sessionId);
        
        public void addMouseDownEvent(string sessionId);
    }
}