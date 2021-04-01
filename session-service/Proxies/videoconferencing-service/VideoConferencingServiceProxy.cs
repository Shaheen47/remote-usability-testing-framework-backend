using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using session_service.Contracts.Proxies;
using session_service.Dtos;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace session_service.Proxies
{
    public class VideoConferencingServiceProxy : IVideoConferencingServiceProxy
    {

        /*private string urlbase = "http://videoconferencing-service/";*/
        private string urlbase = "https://localhost:5003/";
        public async Task<string> createSession()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            String api =urlbase+"Session/create-session";
            StringContent content =
                new StringContent(("{}"), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            ConferenceSession session=JsonConvert.DeserializeObject<ConferenceSession>(responeContent);
            return session.sessionName;
        }

        public async Task stopSession(string sessionId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            String api =urlbase+"Session/"+sessionId;
            var response = await client.DeleteAsync( api);
            var responeContent = await response.Content.ReadAsStringAsync();
        }

        public async Task<string> joinAsModerator(string sessionId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            String api =urlbase+"Session/join-session-moderator";
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
            String api =urlbase+"Session/join-session-participant";
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
            String api =urlbase+"Session/join-session-observer";
            ConferenceSession conferenceSession = new ConferenceSession();
            conferenceSession.sessionName = sessionId;
            StringContent content =
                new StringContent((JsonSerializer.Serialize(conferenceSession)), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            ConferenceToken conferenceToken=JsonConvert.DeserializeObject<ConferenceToken>(responeContent);
            return conferenceToken.token;
        }

        public async Task startRecording(string sessionId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            String api =urlbase+"Recording/start-record";
            ConferenceSession conferenceSession = new ConferenceSession();
            conferenceSession.sessionName = sessionId;
            StringContent content =
                new StringContent((JsonSerializer.Serialize(conferenceSession)), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
        }

        public async Task<string> stopRecording(string sessionId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            String api =urlbase+"Recording/stop-record";
            ConferenceSession conferenceSession = new ConferenceSession();
            conferenceSession.sessionName = sessionId;
            StringContent content =
                new StringContent((JsonSerializer.Serialize(conferenceSession)), Encoding.UTF8, "application/json");
            var response = await client.PostAsync( api, content);
            var responeContent = await response.Content.ReadAsStringAsync();
            VideoRecordingDto videoRecording=JsonConvert.DeserializeObject<VideoRecordingDto>(responeContent);
            return videoRecording.url;
        }
    }
}