using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace screensharing_service.Entities.ScreenMirroring
{
    public class DomInitializationEvent: ScreenMirroringEvent
    {
        [BsonRepresentation(BsonType.String)]
        public string content { get; set; }
    }
}