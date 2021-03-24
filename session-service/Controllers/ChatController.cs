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
        
        
        [HttpGet]
        [Route("get-chat")]
        public async Task<IActionResult> getChat([FromBody] SessionLoginDto loginDto)
        {
            var session =await sessionService.getSession(loginDto.sessionId);
            var messages=chatService.GetChatMessages(session.chatSessionId);
            return Ok(messages);
        }
    }
}