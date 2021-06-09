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
        
        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEvents(string sessionId, EventType eventType)
        {
            var filter = Builders<ScreenReplySession>.Filter.Eq(e => e.sessionId, sessionId);
            var session= await screenReplySessionContext.sessions.FindAsync(filter);
            //ToDo return the required results immediatly from the database
            switch (eventType)
            {
                case EventType.dom:
                    return session.FirstOrDefault().events.AsQueryable().Where(e=>e.GetType()==typeof(DomEvent));
                case EventType.inputChanged:
                    return session.FirstOrDefault().events.AsQueryable().Where(e=>e.GetType()==typeof(InputChangedEvent));
                case EventType.mouseDown:
                    return session.FirstOrDefault().events.AsQueryable().Where(e=>e.GetType()==typeof(MouseDownEvent));
                case EventType.mouseOut:
                    return session.FirstOrDefault().events.AsQueryable().Where(e=>e.GetType()==typeof(MouseOutEvent));
                case EventType.mouseOver:
                    return session.FirstOrDefault().events.AsQueryable().Where(e=>e.GetType()==typeof(MouseOverEvent));
                default:
                    return session.FirstOrDefault().events.AsQueryable().Where(e=>e.GetType()==typeof(MouseUpEvent));
            }
            
            
        }

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEventsStartingFrom(string sessionId, long startTime)
        {
            var events = await getAllEvents(sessionId);
            return events.AsQueryable().Where(e => e.timestamp >= startTime);
        }

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEventsStartingFrom(string sessionId, long startTime, EventType eventType)
        {
            var events = await getAllEvents(sessionId,eventType);
            return events.AsQueryable().Where(e => e.timestamp >= startTime);
        }

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEventsUntil(string sessionId, long stopTime)
        {
            var events = await getAllEvents(sessionId);
            return events.AsQueryable().Where(e => e.timestamp <= stopTime);
        }

        public async Task<IEnumerable<ScreenMirroringEvent>> getAllEventsUntil(string sessionId, long stopTime, EventType eventType)
        {
            var events = await getAllEvents(sessionId,eventType);
            return events.AsQueryable().Where(e => e.timestamp <= stopTime);
        }
    }
}