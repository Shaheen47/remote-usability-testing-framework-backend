using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace screensharing_service.Entities.ScreenMirroring
{
    public class BaseUrlChangedEvent : ScreenMirroringEvent
    {
        [BsonRepresentation(BsonType.String)]
        public string url { get; set; }
    }
}