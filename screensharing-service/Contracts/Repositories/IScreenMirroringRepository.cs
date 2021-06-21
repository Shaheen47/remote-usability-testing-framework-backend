using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using screensharing_service.Entities;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Contracts.Repositories
{
    public interface IScreenMirroringRepository
    {
        
        
        public Task addEvent(ScreenMirroringEvent screenMirroringEvent);
        public Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId);
        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsStartingFrom(string sessionId, long startTime);
        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsUntil(string sessionId,long stopTime);



    }
}