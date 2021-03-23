using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace screensharing_service.Entities.ScreenMirroring
{
    public class DomChange
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String id { get; set; }
        
        public DateTime timestamp { get; set; }

        public string dom { get; set; }
    }
}