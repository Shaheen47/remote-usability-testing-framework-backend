
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Data
{
    public class EventContext: IEventContext
    {
        public EventContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            events = database.GetCollection<ScreenMirroringEvent>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //ScreenMirroringEventContextSeed.SeedData(events);
        }
        
        public IMongoCollection<ScreenMirroringEvent> events { get; }
    }
}