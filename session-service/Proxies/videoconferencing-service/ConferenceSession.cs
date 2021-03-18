using Newtonsoft.Json;

namespace session_service.Proxies
{
    public class ConferenceSession
    {
        [JsonProperty(PropertyName = "sessionId")]
        public string sessionId { get; set; }
    }
}