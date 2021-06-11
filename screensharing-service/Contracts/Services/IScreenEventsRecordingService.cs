using System.Threading.Tasks;
using screensharing_service.Dtos;
using screensharing_service.Entities;

namespace screensharing_service.Contracts.Services
{
    public interface IScreenEventsRecordingService
    {

        public void startSession(string sessionId);
        public void stopSession(string sessionId);
        public Task AddDomInitializationEvent(string sessionId,string content);
        public void AddDomChangeEvent(string sessionId,string content);
        
        public void AddDomClearEvent(string sessionId);
        
        public void AddBaseUrlChangedEvent(string sessionId,string url);
        public void addMouseUpEvent(string sessionId);
        public void addMouseDownEvent(string sessionId);
        public void addMouseOverEvent(string sessionId,string elementXpath);
        public void addMouseOutEvent(string sessionId,string elementXpath);
        public void addInputChangedEvent(string sessionId, string elementXpath, string inputContent);
        
    }
}