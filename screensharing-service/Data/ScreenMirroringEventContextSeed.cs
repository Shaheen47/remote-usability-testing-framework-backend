using MongoDB.Driver;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Data
{
    public class ScreenMirroringEventContextSeed
    {
        public static void seedData(IMongoCollection<ScreenMirroringEvent> eventCollection)
        {
            bool eventExist = eventCollection.Find(p => true).Any();
            if (!eventExist)
            {
                /*eventCollection.InsertManyAsync()*/
            }
        }
    }
}