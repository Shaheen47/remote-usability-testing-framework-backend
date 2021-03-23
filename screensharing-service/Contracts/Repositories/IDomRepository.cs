using System.Collections.Generic;
using screensharing_service.Entities;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Contracts.Repositories
{
    public interface IDomRepository
    {
        public void addDomEvent(DomEvent domEvent,int sessionId);
        public IList<DomEvent> getAllDomEvent(int sessionId);
    }
}