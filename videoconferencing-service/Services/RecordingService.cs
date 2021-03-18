using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using videoconferencing_service.Proxies.OpenVidu;

namespace videoconferencing_service.Services
{
    public class RecordingService : IRecordingService
    {
        
        private readonly IConfiguration Configuration;
        
        private IConferenceProviderProxy conferenceProviderProxy;
        private string openViduUrl;
        private string openViduSecret;
        

        public RecordingService(IConfiguration configuration,IConferenceProviderProxy conferenceProviderProxy)
        {
            Configuration = configuration;
            this.conferenceProviderProxy = conferenceProviderProxy;
            openViduUrl = configuration["OpenVidu:OPENVIDU_URL"];
            openViduSecret = configuration["OpenVidu:SECRET"];

        }
        
        
        public async Task startRecording(string sessionId)
        {
            Session session=conferenceProviderProxy.getSession(sessionId);
            await session.startRecording(openViduUrl, openViduSecret);
        }

        public async Task<string> stopRecording(string sessionId)
        {
            Session session=conferenceProviderProxy.getSession(sessionId);
            Recording recording =await session.stopRecording(openViduUrl, openViduSecret);
            return recording.url;
        }

        public void deleteRecording(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public Recording getRecording(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public List<Recording> getAllRecordings()
        {
            throw new System.NotImplementedException();
        }
    }
}