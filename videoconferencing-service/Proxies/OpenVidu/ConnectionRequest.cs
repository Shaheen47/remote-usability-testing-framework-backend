using Newtonsoft.Json;

namespace videoconferencing_service.Proxies.OpenVidu
{
    public class ConnectionRequest
    {
        [JsonProperty(PropertyName = "type")] 
        public string type { set; get; }

        [JsonProperty(PropertyName = "role")]
        public string role{ set; get; }

        public ConnectionRequest(OpenViduRole role)
        {
            this.role = role.ToString();
            type="WEBRTC";
        }
        
    }
}