using System;

namespace screensharing_service.Entities.ScreenMirroring
{
    public class DomEvent
    {
        public int id { get; set; }
        public DateTime timestamp { get; set; }
        public string content { get; set; }
    }
}