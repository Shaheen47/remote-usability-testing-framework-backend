using System;
using MongoDB.Bson.Serialization.Attributes;

namespace screensharing_service.Entities.ScreenMirroring
{
    public class InitialDom
    {
        [BsonId] public String id { get; set; }
    }
}