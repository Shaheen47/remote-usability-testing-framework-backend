using System.Collections.Generic;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Entities;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Repositories
{
    public class DumbScreenMirroringRepository : IScreenMirroringRepository
    {
        private Dictionary<string, IList<ScreenMirroringEvent>> domStore;
        
        public DumbScreenMirroringRepository()
        {
            this.domStore = new Dictionary<string, IList<ScreenMirroringEvent>>();
        }


        public void addSession(string sessionId)
        {
            this.domStore.Add(sessionId,new List<ScreenMirroringEvent>());
        }

        public void addDomEvent(DomEvent domEvent, string sessionId)
        {
            domStore[sessionId].Add(domEvent);
        }

        public void addMouseMovementEvent(MousePosition mousePosition, string sessionId)
        {
            domStore[sessionId].Add(mousePosition);
        }

        public void addScrollingEvent(ScrollPosition scrollPosition, string sessionId)
        {
            domStore[sessionId].Add(scrollPosition);
        }

        public IList<ScreenMirroringEvent> GetAllScreenMirroringEvents(string sessionId)
        {
            return domStore[sessionId];
        }
        
    }
}