
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Data
{
    public class ScreenReplySessionContext: IScreenReplySessionContext
    {
        public ScreenReplySessionContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            sessions = database.GetCollection<ScreenReplySession>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //ScreenMirroringEventContextSeed.SeedData(events);
        }

        public IMongoCollection<ScreenReplySession> sessions { get; }
    }
}