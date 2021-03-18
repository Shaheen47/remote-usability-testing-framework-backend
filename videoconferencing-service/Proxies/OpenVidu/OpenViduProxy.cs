using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace videoconferencing_service.Proxies.OpenVidu
{
	public class OpenViduProxy : IConferenceProviderProxy
	{
		
		
		private Dictionary<string, Session> activeSessions;
		private readonly IConfiguration Configuration;

		public OpenViduProxy( IConfiguration Configuration)
		{
			this.Configuration = Configuration;
			activeSessions = new Dictionary<string, Session>();
		}

		public async Task<Session> createSession()
		{
			HttpClientHandler clientHandler = new HttpClientHandler();
			clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			HttpClient client = new HttpClient(clientHandler);
			var byteArray = Encoding.ASCII.GetBytes("OPENVIDUAPP"+":"+Configuration["OpenVidu:SECRET"]);
			client.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
			String api =Configuration["OpenVidu:OPENVIDU_URL"]+ Configuration["OpenVidu:OpenViduApi:API_SESSIONS"];
			StringContent content =
				new StringContent(("{}"), Encoding.UTF8, "application/json");
			var response = await client.PostAsync( api, content);
			var responeContent = await response.Content.ReadAsStringAsync();
			Session session=JsonConvert.DeserializeObject<Session>(responeContent);
			this.activeSessions[session.sessionId] = session;
			
			return session;
		}

		
		public async Task<Session> refreshSessionInfo(string sessionId)
		{
			HttpClientHandler clientHandler = new HttpClientHandler();
			clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			HttpClient client = new HttpClient(clientHandler);
			var byteArray = Encoding.ASCII.GetBytes("OPENVIDUAPP"+":"+Configuration["OpenVidu:SECRET"]);
			client.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
			String api =Configuration["OpenVidu:OPENVIDU_URL"]+ Configuration["OpenVidu:OpenViduApi:API_SESSIONS"];
			var response = await client.GetAsync(api+sessionId);
			var responeContent = await response.Content.ReadAsStringAsync();
			Session session=JsonConvert.DeserializeObject<Session>(responeContent);
			this.activeSessions[session.sessionId] = session;
			return session;
		}

		public Session getSession(string sessionId)
		{
			return activeSessions[sessionId];
		}

		public async Task endSession(string sessionId)
		{
			HttpClientHandler clientHandler = new HttpClientHandler();
			clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			HttpClient client = new HttpClient(clientHandler);
			var byteArray = Encoding.ASCII.GetBytes("OPENVIDUAPP"+":"+Configuration["OpenVidu:SECRET"]);
			client.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
			String api =Configuration["OpenVidu:OPENVIDU_URL"]+ Configuration["OpenVidu:OpenViduApi:API_SESSIONS"];
			await client.DeleteAsync(api+sessionId);
		}
		
		
	}

};