using System.Collections.Generic;
using screensharing_service.Entities;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Contracts.Repositories
{
    public interface IScreenMirroringRepository
    {
        
        public void addSession(string sessionId);
        public void addDomEvent(DomEvent domEvent,string sessionId);

        public void addMouseMovementEvent(MousePosition mousePosition, string sessionId);

        public void addScrollingEvent(ScrollPosition scrollPosition, string sessionId);
        
        public void addMouseUpEvent(MouseUpEvent mouseUpEvent,string sessionId);
        
        public void addMouseDownEvent(MouseDownEvent mouseDownEvent,string sessionId);
        
        public IList<ScreenMirroringEvent> GetAllScreenMirroringEventsSortedByTimestamp(string sessionId);
    }
}