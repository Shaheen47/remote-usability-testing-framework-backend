using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using session_service.Contracts.Proxies;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace session_service.Proxies
{
    public class VideoConferencingServiceProxy : IVideoConferencingServiceProxy
    {
        public async Task<string> createSession()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            String api ="https://localhost:5003/Session/create-session";
            StringContent content =
                new StringContent(("{}"), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            ConferenceSession session=JsonConvert.DeserializeObject<ConferenceSession>(responeContent);
            return session.sessionName;
        }

        public Task stopSession(string sessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> joinAsModerator(string sessionId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            String api ="https://localhost:5003/Session/join-session-moderator";
            ConferenceSession conferenceSession = new ConferenceSession();
            conferenceSession.sessionName = sessionId;
            StringContent content =
                new StringContent((JsonSerializer.Serialize(conferenceSession)), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            ConferenceToken conferenceToken=JsonConvert.DeserializeObject<ConferenceToken>(responeContent);
            return conferenceToken.token;
        }

        public async Task<string> joinAsParticipant(string sessionId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            String api ="https://localhost:5003/Session/join-session-participant";
            ConferenceSession conferenceSession = new ConferenceSession();
            conferenceSession.sessionName = sessionId;
            StringContent content =
                new StringContent((JsonSerializer.Serialize(conferenceSession)), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            ConferenceToken conferenceToken=JsonConvert.DeserializeObject<ConferenceToken>(responeContent);
            return conferenceToken.token;
        }

        public async Task<string> joinAsObserver(string sessionId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            String api ="https://localhost:5003/Session/join-session-observer";
            ConferenceSession conferenceSession = new ConferenceSession();
            conferenceSession.sessionName = sessionId;
            StringContent content =
                new StringContent((JsonSerializer.Serialize(conferenceSession)), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            ConferenceToken conferenceToken=JsonConvert.DeserializeObject<ConferenceToken>(responeContent);
            return conferenceToken.token;
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