using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace videoconferencing_service.Proxies.OpenVidu
{
    public class Session
    {

        [JsonProperty(PropertyName = "id")]
        public String sessionId { get; set; }
        
        [JsonProperty(PropertyName = "createdAt")]
        public long createdAt{ get; set; }
        
        [JsonProperty(PropertyName = "recordingMode")]
        public RecordingMode recordingMode{ get; set; }
        
        [JsonProperty(PropertyName = "recording")]
        public bool recording{ get; set; }
        
        
        public Recording theRecording { get; set; }

        

        public async Task<Connection> createConnection(OpenViduRole role,string openViduUrl,string openViduSecret)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);

            ConnectionRequest request = new ConnectionRequest(role);
            var byteArray = Encoding.ASCII.GetBytes("OPENVIDUAPP"+":"+openViduSecret);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            String api =openViduUrl+ "/openvidu/api/sessions/" + this.sessionId + "/connection";
            StringContent content =
                new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            Connection connection = JsonConvert.DeserializeObject<Connection>(responeContent);
            
            return connection;

        }

        public async Task<Connection> getConnection(string connectionId,string openViduUrl,string openViduSecret)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            var byteArray = Encoding.ASCII.GetBytes("OPENVIDUAPP"+":"+openViduSecret);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            String api = openViduUrl + "/openvidu/api/sessions/";
            var response = await client.GetAsync(api+sessionId+"connection/"+connectionId);
            var responeContent = await response.Content.ReadAsStringAsync();
            Connection connection=JsonConvert.DeserializeObject<Connection>(responeContent);
            return connection;
        }
        
        public async Task deleteConnection(string connectionId,string openViduUrl,string openViduSecret)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            var byteArray = Encoding.ASCII.GetBytes("OPENVIDUAPP"+":"+openViduSecret);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            String api =openViduUrl+ "/openvidu/api/sessions/"; 
            await client.DeleteAsync(api+sessionId+"connection/"+connectionId);
        }


        public async Task<Recording> startRecording(string openViduUrl, string openViduSecret)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);

            RecordingRequest request = new RecordingRequest(sessionId,sessionId+"_recording");
            var byteArray = Encoding.ASCII.GetBytes("OPENVIDUAPP"+":"+openViduSecret);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            String api =openViduUrl+ "/openvidu/api/recordings/start";
            var x = JsonSerializer.Serialize(request);
            StringContent content =
                new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            Recording recording=JsonConvert.DeserializeObject<Recording>(responeContent);
            theRecording = recording;
            return recording;
        }
        
        public async Task<Recording> stopRecording(string openViduUrl, string openViduSecret)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            
            var byteArray = Encoding.ASCII.GetBytes("OPENVIDUAPP"+":"+openViduSecret);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            String api =openViduUrl+ "/openvidu/api/recordings/stop/"+theRecording.id;
            StringContent content =
                new StringContent("{}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            Recording recording=JsonConvert.DeserializeObject<Recording>(responeContent);
            theRecording = recording;
            return recording;
            
        }


    }
}