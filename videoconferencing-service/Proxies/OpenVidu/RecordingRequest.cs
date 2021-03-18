using Newtonsoft.Json;

namespace videoconferencing_service.Proxies.OpenVidu
{
    public class RecordingRequest
    {
        public RecordingRequest(string sessionId, string name)
        {
            this.session = sessionId;
            this.name = name;
        }

        [JsonProperty(PropertyName = "session")] 
        public string session { get; set; }
        
        [JsonProperty(PropertyName = "name")] 
        public string name { get; set; }
    }
}