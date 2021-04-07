using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using screensharing_service.Entities;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Contracts.Repositories
{
    public interface IScreenMirroringRepository
    {
        
        
        /*public IList<ScreenMirroringEvent> GetAllScreenMirroringEventsSortedByTimestamp(string sessionId);*/

        public Task CreateSession(string sessionId);
        public Task<ScreenReplySession> getSession(string sessionId);
        
        public Task addEvent(ScreenMirroringEvent screenMirroringEvent,string sessionId);
        public Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId, EventType eventType);
        public Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId);
        public Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId, long startTime, long stopTime);
        public Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId, long startTime, long stopTime, EventType eventType);
        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsStartingFrom(string sessionId, long startTime);
        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsStartingFrom(string sessionId, long startTime, EventType eventType);
        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsUntil(string sessionId,long stopTime);
        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsUntil(string sessionId,long stopTime, EventType eventType);



    }
}