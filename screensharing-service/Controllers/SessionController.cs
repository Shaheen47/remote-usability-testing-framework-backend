using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using screensharing_service.Contracts.Services;
using screensharing_service.Dtos;
using screensharing_service.Entities;

namespace screensharing_service.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class SessionController: ControllerBase
    {
        private ISessionService sessionService;
        private IScreenEventsReplyService screenEventsReplyService;

        public SessionController(ISessionService sessionService,IScreenEventsReplyService screenEventsReplyService)
        {
            this.sessionService = sessionService;
            this.screenEventsReplyService = screenEventsReplyService;
        }

        [HttpPost]
        [Route("create-session")]
        public async Task<IActionResult> createSession()
        {
            Session session=await sessionService.createSession();
            return Created("session",session);
        }
        
        
        [HttpPost]
        [Route("reply-session")]
        public async Task<IActionResult> replySession([FromBody] SessionLoginDto loginDto)
        {
            //check session
            
            //reply
           screenEventsReplyService.startSessionReply(loginDto.sessionId);
            return Ok("session");
        }
    }
}