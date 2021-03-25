using System.Threading.Tasks;

namespace screensharing_service.Hubs
{
    public interface IScreenMirroringHub
    {
        public Task joinSessionAsSubscriber(string sessionId);

        public Task joinSessionAsPublisher(string sessionId);

        public Task leaveSession(string sessionId);

        public Task sendDom(string sessionId, string dom);

        public Task sendMousePosition(string sessionId, int x, int y);

        public Task sendScroll(string sessionId, int vertical);
    }
}