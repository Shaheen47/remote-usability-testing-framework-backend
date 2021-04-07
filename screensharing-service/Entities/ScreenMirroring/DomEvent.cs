using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace screensharing_service.Entities.ScreenMirroring
{
    public class DomEvent: ScreenMirroringEvent
    {
        [BsonRepresentation(BsonType.String)]
        public string content { get; set; }
        
    }
}