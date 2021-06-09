using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace screensharing_service.Entities.ScreenMirroring
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(DomEvent),typeof(InputChangedEvent),typeof(MouseOverEvent),typeof(MouseOutEvent),typeof(MouseUpEvent),typeof(MouseDownEvent))]
    public class ScreenMirroringEvent
    {
        [BsonRepresentation(BsonType.Double)]
        public double timestamp { get; set; }
        
    }
}