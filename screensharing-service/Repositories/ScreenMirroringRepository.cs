using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Data;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Repositories
{
    public class ScreenMirroringRepository:IScreenMirroringRepository
    {
        private readonly IEventContext eventContext;

        public ScreenMirroringRepository(IEventContext eventContext)
        {
            this.eventContext = eventContext;
        }
        

        public async Task addEvent(ScreenMirroringEvent screenMirroringEvent)
        {
            await eventContext.events.InsertOneAsync(screenMirroringEvent);

        }
        

        public  async Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId)
        {
            var events=  eventContext.events
                .Find(e => e.sessionId==sessionId)
                .ToEnumerable();
            return events;
        }
        

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEventsStartingFrom(string sessionId, long startTime)
        {
            var events=  eventContext.events
                .Find(e => e.sessionId==sessionId &&  e.timestamp >= startTime)
                .ToEnumerable();
            return events;
        }
        

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEventsUntil(string sessionId, long stopTime)
        {
            var events=  eventContext.events
                .Find(e => e.sessionId==sessionId &&  e.timestamp <= stopTime)
                .ToEnumerable();
            return events;
        }
        
    }
}