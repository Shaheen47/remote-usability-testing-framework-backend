using System.Collections.Generic;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Entities;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Repositories
{
    public class DumbDomRepository : IDomRepository
    {
        public DumbDomRepository()
        {
            this.domStore = new Dictionary<int, IList<DomEvent>>();
        }

        private Dictionary<int, IList<DomEvent>> domStore;
        
        
        public void addDomEvent(DomEvent domEvent, int sessionId)
        {
            domStore[sessionId].Add(domEvent);
        }

        public IList<DomEvent> getAllDomEvent(int sessionId)
        {
            return domStore[sessionId];
        }
    }
}