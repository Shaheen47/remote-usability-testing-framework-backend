namespace screensharing_service.Entities.ScreenMirroring
{
    public class ScrollPosition:ScreenMirroringEvent
    {
        public int vertical { get; set; }
        public int horizonal { get; set; }
    }
}