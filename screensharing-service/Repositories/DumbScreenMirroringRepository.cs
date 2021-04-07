using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Entities;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Repositories
{
    public class DumbScreenMirroringRepository : IScreenMirroringRepository
    {
        private Dictionary<string, ScreenReplySession> eventStore;
        
        public DumbScreenMirroringRepository()
        {
            this.eventStore = new Dictionary<string, ScreenReplySession>();
        }
        

        public async Task CreateSession(string sessionId)
        {
            eventStore.Add(sessionId,new ScreenReplySession(){sessionId = sessionId});
        }

        public Task<ScreenReplySession> getSession(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public async Task addEvent(ScreenMirroringEvent screenMirroringEvent, string sessionId)
        {
            eventStore[sessionId].events.Add(screenMirroringEvent);
        }

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId)
        {
            return eventStore[sessionId].events.OrderBy(p=>p.timestamp).ToList();
        }

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId, long startTime, long stopTime)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId, long startTime, long stopTime, EventType eventType)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId, EventType eventType)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsStartingFrom(string sessionId, long startTime)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsStartingFrom(string sessionId, long startTime, EventType eventType)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsUntil(string sessionId, long stopTime)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsUntil(string sessionId, long stopTime, EventType eventType)
        {
            throw new System.NotImplementedException();
        }
    }
}