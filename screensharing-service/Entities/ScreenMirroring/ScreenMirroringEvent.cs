using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace screensharing_service.Entities.ScreenMirroring
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(DomInitializationEvent),typeof(DomChangeEvent),typeof(InputChangedEvent),typeof(MouseOverEvent),typeof(MouseOutEvent),typeof(MouseUpEvent),typeof(MouseDownEvent))]
    public class ScreenMirroringEvent
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        
        [BsonRepresentation(BsonType.String)]
        public string sessionId { get; set; }
        
        [BsonRepresentation(BsonType.Double)]
        public double timestamp { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]  // JSON.Net
        [BsonRepresentation(BsonType.String)]         // Mongo
        public EventType eventType{ get; set; }
        
    }
}