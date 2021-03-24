using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using session_service.Contracts.Services;

namespace session_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController:ControllerBase
    {
        public ChatController(IChatService chatService, ISessionService sessionService)
        {
            this.chatService = chatService;
            this.sessionService = sessionService;
        }

        private IChatService chatService;
        private ISessionService sessionService;
        
        
        [HttpGet("{sessionId}")]
        public async Task<IActionResult> getChat(string sessionId)
        {
            var session =await sessionService.getSession(sessionId);
            var messages=chatService.GetChatMessages(session.chatSessionId);
            return Ok(messages);
        }
    }
}