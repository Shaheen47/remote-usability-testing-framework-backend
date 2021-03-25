using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using session_service.Contracts.Proxies;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace session_service.Proxies
{
    public class ScreensharingServiceProxy: IScreensharingServiceProxy
    {
        
        public async Task<ScreensharingSession> createSession()
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            /*String api ="http://screensharing-service:80/Session/create-session";*/
            String api ="https://localhost:5005/Session/create-session";
            StringContent content =
                new StringContent(("{}"), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            ScreensharingSession session=JsonConvert.DeserializeObject<ScreensharingSession>(responeContent);
            return session;
            
        }

        public async Task<RecordedScreensharingSession> createSessionWithRecording()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            /*String api ="http://screensharing-service:80/Session/create-session";*/
            String api ="https://localhost:5005/Session/create-session-with-recording";
            StringContent content =
                new StringContent(("{}"), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            RecordedScreensharingSession session=JsonConvert.DeserializeObject<RecordedScreensharingSession>(responeContent);
            return session;
        }

        public async Task stopSession(string sessionId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            /*String api ="http://screensharing-service:80/Session/create-session";*/
            String api ="https://localhost:5005/Session/stop-session"+sessionId;
            var response = await client.DeleteAsync( api);
            var responeContent = await response.Content.ReadAsStringAsync();
        }
        

        public async Task replySession(string sessionId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            /*String api ="http://screensharing-service:80/Session/create-session";*/
            String api ="https://localhost:5005/Session/reply-session";
            SessionLoginDto loginDto = new SessionLoginDto();
            loginDto.sessionId = sessionId;
            StringContent content =
                new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
        }
    }
}