namespace screensharing_service.Entities.ScreenMirroring
{
    public class MousePosition:ScreenMirroringEvent
    {
        public int top { get; set; }
        public int left { get; set; }
    }
}