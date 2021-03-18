using System.Collections.Generic;
using System.Threading.Tasks;
using videoconferencing_service.Proxies.OpenVidu;

namespace videoconferencing_service.Services
{
    public interface IRecordingService
    {
        public Task startRecording(string sessionId);

        public Task<string> stopRecording(string sessionId);
        
        public void deleteRecording(string sessionId);

        public Recording getRecording(string sessionId);

        public List<Recording> getAllRecordings();
    }
}