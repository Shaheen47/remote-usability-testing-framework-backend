using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace screensharing_service.Entities.ScreenMirroring
{
    public class ScreenReplySession
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        
        [BsonRepresentation(BsonType.String)]
        public string sessionId { get; set; }
        
        
        public List<ScreenMirroringEvent> events { get; set; }
        
        public ScreenReplySession()
        {
            events = new List<ScreenMirroringEvent>();
        }
    }
}