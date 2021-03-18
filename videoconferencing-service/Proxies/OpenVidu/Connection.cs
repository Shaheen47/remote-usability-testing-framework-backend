using System;
using Newtonsoft.Json;

namespace videoconferencing_service.Proxies.OpenVidu
{
    public class Connection
    {
        
        
        [JsonProperty(PropertyName = "id")]
        private String connectionId;
        
        [JsonProperty(PropertyName = "status")]
        private String status;
        
        [JsonProperty(PropertyName = "createdAt")]
        private long createdAt;
        
        /*[JsonProperty(PropertyName = "activeAt")]
        private long activeAt;*/

        [JsonProperty(PropertyName = "platform")]
        private String platform;
        
        [JsonProperty(PropertyName = "clientData")]
        private String clientData;
        
        [JsonProperty(PropertyName = "token")]
        private String token;
        
        [JsonProperty(PropertyName = "role")]
        private OpenViduRole role;

        

   
        

        public OpenViduRole Role
        {
            get => role;
            set => role = value;
        }

        public string Token
        {
            get => token;
            set => token = value;
        }

        public string ClientData
        {
            get => clientData;
            set => clientData = value;
        }

        public string Platform
        {
            get => platform;
            set => platform = value;
        }
        
        

        public long CreatedAt
        {
            get => createdAt;
            set => createdAt = value;
        }

        public string Status
        {
            get => status;
            set => status = value;
        }

        public string ConnectionId
        {
            get => connectionId;
            set => connectionId = value;
        }
    }
}