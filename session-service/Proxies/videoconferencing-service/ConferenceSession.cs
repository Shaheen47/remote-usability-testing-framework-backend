using Newtonsoft.Json;

namespace session_service.Proxies
{
    public class ConferenceSession
    {
        [JsonProperty(PropertyName = "sessionName")]
        public string sessionName { get; set; }
    }
}