using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace screensharing_service.Entities.ScreenMirroring
{
    public class DomInitializationEvent: ScreenMirroringEvent
    {
        [BsonRepresentation(BsonType.String)]
        public string content { get; set; }
        
        [BsonRepresentation(BsonType.String)]
        public string baseUrl { get; set; }
    }
}