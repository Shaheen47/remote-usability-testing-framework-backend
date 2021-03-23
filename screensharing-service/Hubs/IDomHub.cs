using System.Threading.Tasks;

namespace screensharing_service.Hubs
{
    public interface IDomHub
    {
        public Task joinSession(string sessionId);
        public Task leaveSession(string sessionId);
        public Task sendDom(string sessionId, string dom);
        public Task sendMousePosition(string sessionId, int x, int y);
        public Task sendScroll(string sessionId, int vertical);

    }
}