using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using session_service.Contracts.Proxies;

namespace session_service.Proxies
{
    public class ScreensharingServiceProxy: IScreensharingServiceProxy
    {
        
        public async Task<ScreensharingSession> createSession()
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            String api ="http://screensharing-service:80/Session/create-session";
            StringContent content =
                new StringContent(("{}"), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            ScreensharingSession session=JsonConvert.DeserializeObject<ScreensharingSession>(responeContent);
            return session;
            
        }

        public Task stopSession(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public Task startRecording(string sessionId)
        {
            throw new System.NotImplementedException();
        }

        public Task stopRecording(string sessionId)
        {
            throw new System.NotImplementedException();
        }
    }
}