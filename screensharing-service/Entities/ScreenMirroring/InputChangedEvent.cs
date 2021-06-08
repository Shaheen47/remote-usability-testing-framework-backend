namespace screensharing_service.Entities.ScreenMirroring
{
    public class InputChangedEvent:ScreenMirroringEvent
    {
        public string elementXpath { get; set; }
        public string content { get; set; }
    }
}