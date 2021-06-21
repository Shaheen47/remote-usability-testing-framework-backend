
using MongoDB.Driver;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Data
{
    public interface IEventContext
    {
        IMongoCollection<ScreenMirroringEvent> events { get; }
    }
}