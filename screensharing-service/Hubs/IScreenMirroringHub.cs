using System.Threading.Tasks;

namespace screensharing_service.Hubs
{
    public interface IScreenMirroringHub
    {
        public Task closeSession(string sessionId);
        public Task joinSessionAsSubscriber(string sessionId);

        public Task joinSessionAsPublisher(string sessionId);

        public Task leaveSession(string sessionId);

        public Task sendDom(string sessionId, string dom);

        public Task mouseUp(string sessionId);

        public Task mouseDown(string sessionId);

        public Task mouseOver(string sessionId, string elementXpath);
        
        public Task mouseOut(string sessionId, string elementXpath);
        
        public Task inputChanged(string sessionId, string elementXpath,string inputContent);
    }
}