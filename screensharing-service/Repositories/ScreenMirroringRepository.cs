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
        private readonly IScreenReplySessionContext screenReplySessionContext;

        public ScreenMirroringRepository(IScreenReplySessionContext screenReplySessionContext)
        {
            this.screenReplySessionContext = screenReplySessionContext;
        }

        public async Task CreateSession(string sessionId)
        {
            var session = new ScreenReplySession {sessionId = sessionId};
            await screenReplySessionContext.sessions.InsertOneAsync(session);
        }

        public async Task<ScreenReplySession> getSession(string sessionId)
        {
            return await screenReplySessionContext.sessions 
                .Find(p => p.id==sessionId)
                .FirstOrDefaultAsync();
        }

        public async Task addEvent(ScreenMirroringEvent screenMirroringEvent, string sessionId)
        {
            var filter = Builders<ScreenReplySession>.Filter.Eq(e => e.sessionId, sessionId);
            var update = Builders<ScreenReplySession>.Update.Push(e => e.events, screenMirroringEvent);
            await screenReplySessionContext.sessions.FindOneAndUpdateAsync(filter, update);

        }

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId)
        {
            var session= await screenReplySessionContext.sessions
                .Find(p => p.sessionId==sessionId)
                .FirstOrDefaultAsync();
            return session.events;
        }

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId, long startTime, long stopTime)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId, long startTime, long stopTime, EventType eventType)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId, EventType eventType)
        {
            var discriminatorFieldDefinition = new StringFieldDefinition<ScreenMirroringEvent, string>("_t");
            var f1 = new StringFieldDefinition<ScreenMirroringEvent, string>("_t");
            
            var filter = Builders<ScreenReplySession>.Filter.Eq(e => e.sessionId, sessionId)
                         & Builders<ScreenReplySession>.Filter.ElemMatch(e =>e.events, Builders<ScreenMirroringEvent>
                             .Filter.OfType<DomEvent>());
            var session= await screenReplySessionContext.sessions.FindAsync(filter);
            return session.FirstOrDefault().events;
        }

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEventsStartingFrom(string sessionId, long startTime)
        {
            var filter = Builders<ScreenReplySession>.Filter.Eq(e => e.sessionId, sessionId)
                         & Builders<ScreenReplySession>.Filter.ElemMatch(e =>e.events, Builders<ScreenMirroringEvent>
                             .Filter.Gte(e=>e.timestamp,startTime));
            var session= await screenReplySessionContext.sessions.FindAsync(filter);
            return session.FirstOrDefault().events;
        }

        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsStartingFrom(string sessionId, long startTime, EventType eventType)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEventsUntil(string sessionId, long stopTime)
        {
            var filter = Builders<ScreenReplySession>.Filter.Eq(e => e.sessionId, sessionId)
                         & Builders<ScreenReplySession>.Filter.ElemMatch(e =>e.events, Builders<ScreenMirroringEvent>
                             .Filter.Lte(e=>e.timestamp,stopTime));
            var session= await screenReplySessionContext.sessions.FindAsync(filter);
            return session.FirstOrDefault().events;
        }

        public Task<IEnumerable<ScreenMirroringEvent>> getAllEventsUntil(string sessionId, long stopTime, EventType eventType)
        {
            throw new System.NotImplementedException();
        }
    }
}